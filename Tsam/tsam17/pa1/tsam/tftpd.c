#include <sys/time.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <string.h>
#include <ctype.h>
#include <stdio.h>
#include <errno.h>
#include <stdlib.h>
#include <limits.h>
#include <arpa/inet.h>
#include <dirent.h>
#include <unistd.h>


struct RRQ
{
    unsigned short opcode;
    char filename[256];
};

struct ACK
{
    unsigned short opcode;
    unsigned short block;
};

struct DATA
{
    unsigned short opcode;
    unsigned short block;
    char data[512];
};

struct ERR
{
    unsigned short opcode;
    unsigned short errorcode;
    char errmsg[512];
};

void CriticalError(char *message)
{
    fprintf(stderr, "Error %s\n", message);
    exit(0);
};

// Checks if argv[1] is a valid directory
int checkIfDirectory(char* dirName)
{
    struct stat path_stat;
    stat(dirName, &path_stat);
    if (S_ISDIR(path_stat.st_mode))
    {
        return 1;
    }
    else
    {
        return -1;
    }
}

// Checks if client download file is in correct directory
int CheckIfDirInCurrentDirectory(char* Client,char* Server)
{
    struct dirent *de;

    //opening the working dir of the server.
    DIR *dir = opendir(Server);
    if (dir == NULL)
    {
        return 0;
    }

    // compares the client request with all files in the working directory
    // Returns True if the file is valid.
    while ((de = readdir(dir)) != NULL)
    {
        if (strcmp(de->d_name, Client) == 0)
        {
            closedir(dir);
            return 1;
        }
    }
    return 0;
}

int main(int argc, char**argv)
{
    char* path;
    char *p;
    char* client_ip;
    size_t path_size;

    int sockfd;
    socklen_t client_len;

    struct sockaddr_in server, client;
    struct RRQ readrequest;
    struct ERR errormessage;
    struct DATA datablock;
    struct ACK ackmessage;
    struct timeval timeout;

    FILE *file;
    unsigned short sendsize;
    int counter;

    //timeout settings
    timeout.tv_sec = 10;  //seconds
    timeout.tv_usec = 0;  //microseconds

    // Check if correct amount of arguments are entered when starting up the server.
    if (argc < 3 || argc > 3)
    {
        char *msg = (char*) malloc(400);
        strcpy(msg, "Invalid Number of Arguments must be 2 arguments, port number and directory name");
        CriticalError(msg);
        free(msg);
    }

    // Convert string to unsigned short for port
    unsigned short port = ((unsigned short)strtol(argv[1], &p, 10));

    // Checking if source is a directory.
    if (checkIfDirectory(argv[2]) < 0)
    {
        char *msg = (char*) malloc(30);
        strcpy(msg, "path is not a directory");
        CriticalError(msg);
        free(msg);
    }

    while(1)
    {
        sockfd = socket(AF_INET, SOCK_DGRAM, 0);
        if (sockfd < 0)
        {
            CriticalError("failed to create socket");
        }

        //Initialize server.
        memset(&server, 0, sizeof(server));
        server.sin_family = AF_INET;
        server.sin_port = htons(port);
        server.sin_addr.s_addr = htonl(INADDR_ANY);


        // Try to bind to client if not success we terminate the program.
        int error = bind(sockfd, (struct sockaddr *) &server, (socklen_t) sizeof(server));;
        if (error < 0)
        {
            CriticalError("did not success binding socket to port.");
        }

        client_len = (socklen_t) sizeof(client);
        fprintf(stdout,"\nlistening to port %hu \n\n", port);

        //Recieves from client readrequest package
        ssize_t n = recvfrom(sockfd, (char *)&readrequest, sizeof(readrequest), 0, (struct sockaddr *)&client, &client_len);
        if (n < 0)
        {
            CriticalError("No package received");
        }

        //Check if the package from the client is other than read request.
        if(htons(readrequest.opcode) != 1)
        {
            char msg[35] = "Writing to directory not allowed!";
            strcpy(errormessage.errmsg, msg);
            errormessage.opcode = htons(5);
            errormessage.errorcode = htons(1);
            sendto(sockfd, &errormessage, 512 + 4, 0, (struct sockaddr *)&client, client_len);

            fprintf(stdout, "%s\n",msg);
            close(sockfd);
            continue;
        }

        // Get the client IP information
        client_ip = inet_ntoa(client.sin_addr);
        fprintf(stdout, "Package recieved from: %s on port %u \n", client_ip, client.sin_port);
        fprintf(stdout, "User requested the file: %s\n", readrequest.filename);

        //Open file in specific directory
        char* dir;
        char* concat = "./";
        dir = malloc(strlen(argv[2]) + strlen(concat) + strlen("/") + 1);

        strcpy(dir, concat);
        strcat(dir, argv[2]);
        strcat(dir, "/");

        //size of the path that fopen has to open.
        path_size = strlen(dir) + strlen(readrequest.filename) + 1;
        //allocating the size in memory
        path = malloc(path_size);
        //creating the file path by joining dir and filename
        snprintf(path, path_size, "%s%s", dir, readrequest.filename);

        // Open directory and if not possible we send to client that file was not found.
        file = fopen(path, "r+");

        // If file is null or the path starts with '.' or '/' then we assume the client is trying to get
        // data that he is not allowed to get. So we send error "File not found" to the client.
        if (file == NULL || !CheckIfDirInCurrentDirectory(readrequest.filename, dir))
        {
            char msg[15] = "File not found";
            strcpy(errormessage.errmsg, msg);
            errormessage.opcode = htons(5);
            errormessage.errorcode = htons(1);
            sendto(sockfd, &errormessage, 512 + 4, 0, (struct sockaddr *)&client, client_len);

            fprintf(stdout, "%s\n",msg);
            close(sockfd);
            continue;
        }
        free(path);
        free(dir);

        fprintf(stdout, "File found\n");
        // TRANSFER DATA
        counter = 1;
        int filesize = 0;

        //sock timeout
        setsockopt(sockfd, SOL_SOCKET, SO_RCVTIMEO, &timeout, sizeof(timeout));
        // Here we start transfer between client and server.
        while(1)
        {
            // sendsize = number of bytes that was read from the file.
            // fread reads the content from the file into datablock.data, 1 byte at a time, 512 times.
            sendsize = fread(datablock.data, 1, 512, file);
            datablock.opcode = htons(3);
            datablock.block = htons(counter);

            // If data package does not match we will retransmit the same package 3 times until we get an
            // answer that the right package was recieved.
            int resend = 0;
            while (resend < 4)
            {
                // Sending the data to the client
                sendto(sockfd, &datablock, sendsize + 4, 0, (struct sockaddr *)&client, client_len);
                // Receiving ACK from the client
                ssize_t y = recvfrom(sockfd, (char *)&ackmessage, sizeof(ackmessage), 0, (struct sockaddr *)&server, &client_len);

                // If no package is received we terminate the program
                if (y < 0)
                {
                    CriticalError("No package received");
                }
                // If error package was sent we terminate the connection.
                if (htons(ackmessage.opcode) == 5)
                {
                    fprintf(stdout, "Recieved error from client, closing the connection!");
                    sendsize = 0;
                }
                // Stop the resend loop if the client received the file successfully.
                if (y == 4)
                {
                    break;
                }
                resend++;
            }

            filesize += sendsize;
            counter++;

            // deleting the data from datablock.data
            memset(&datablock.data[0], 0, sizeof(datablock.data));

            // If sendsize != 512 then it is the last package to be sent and we close the connection.
            if (sendsize != 512)
            {
                fprintf(stdout, "File sent successfully \n");
                fprintf(stdout, "%s %d %s \n", "Total data uploaded: ", filesize, "Bytes" );
                fflush(stdout);
                close(sockfd);
                fclose(file);
                break;
            }
        }
        close(sockfd);
    }
    return 0;
}
