public class CalcTest
{
    public static void main(String[] args)
    {
        Calculator calc = new Calculator();

        String[] exp =  { "4", "6", "5","5","*","+","/" };
        System.out.println(calc.evalRPN(exp));

    }
}
