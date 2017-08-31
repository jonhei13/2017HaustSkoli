/*
  *  @(#) Hello.java 1.8 23 Aug 2017 Jon Heidar Sigmundsson
  *  Copyright(c) Jon Heidar Sigmundsson.
 */
import java.util.ArrayList;

/**
 * Calculates numbers with operators (/,*,-,+)
 */

public class Calculator {

    /**
     * Takes list of operators and list of numbers.
     * calculates  the results and returns.
     */
    private Double calculate(ArrayList<String> operators, ArrayList<Double> numbers){
        double result = 0.0;
        String operator = "";
        boolean firstOccurance = true;

        double x = 0.0;
        double y = numbers.get(numbers.size()-1);
        numbers.remove(numbers.get(numbers.size()-1));
        for (int i = numbers.size();  i > 0; i--)
        {
            x = numbers.get(numbers.size()-1);
            numbers.remove(numbers.get(numbers.size()-1));
            operator = operators.get(operators.size()-1);
            operator = operators.remove(operators.size()-1);
            if (operator.equals("+"))
            {
                if (firstOccurance)
                {
                    result = x + y;
                    firstOccurance = false;
                }
                else
                {
                    result += x;
                }
            }
            else if(operator.equals("-"))
            {
                if (firstOccurance)
                {
                    result = x - y;
                    firstOccurance = false;
                }
                else
                {
                    result -= x;
                }
            }
            else if (operator.equals("/"))
            {
                if (firstOccurance)
                {
                    result = x / y;
                    firstOccurance = false;
                }
                else
                {
                    result /= x;
                }
            }
            else if (operator.equals("*"))
            {
                if (firstOccurance)
                {
                    result = x * y;
                    firstOccurance = false;
                }
                else
                {
                    result *= x;
                }
            }
        }
        return result;
    }

    // Returns list of operators in String[]
    private ArrayList<String> GetOperators(String[] expression){
        ArrayList<String> result = new ArrayList<>();
        for (int i = 0; i < expression.length; i++){
            if (!(expression[i].matches("[0-9]"))){
                result.add(expression[i]);
            }
        }
        return result;

    }
    private ArrayList<Double>GetNumbers(String[] expression){
        ArrayList<Double> result = new ArrayList<>();
        for (int i = 0; i < expression.length; i++){
            if (expression[i].matches("[0-9]")){
                result.add(Double.parseDouble(expression[i]));
            }
        }
        return result;
    }

    /**
     * Takes in string array and calculates the numbers together
     * with (+,-,/,*) operators.
     * of format "1,2,3,4,+,*,+".
     */
    public double evalRPN(String[] exp){
        ArrayList<String> operators = GetOperators(exp);
        ArrayList<Double> numbers = GetNumbers(exp);
        if (operators.size() <= numbers.size())
        {
            return calculate(operators, numbers);
        }
        else{
            System.out.println("Invalid Format");
        }
        return 0.0;

    }
}
