/*
  *  @(#) Hello.java 1.8 23 Aug 2017 Jon Heidar Sigmundsson
  *  Copyright(c) Jon Heidar Sigmundsson.
 */

import com.sun.org.apache.bcel.internal.generic.ARRAYLENGTH;

import java.util.ArrayList;

/**
 * Calculates numbers with operators (/,*,-,+)
 */

public class Calculator {

    /**
     * takes string array and calculates the numbers.
     * returns a the results in double format.
     * (+, -, /, *.)
     */
    private Double calculate(ArrayList<String> operators, ArrayList<Double> numbers){


        return 0.0;
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
     * of format "2,3,4,5,+" where last is the expression.
     */
    public double evalRPN(String[] exp){
        ArrayList<String> operators = GetOperators(exp);
        ArrayList<Double> numbers = GetNumbers(exp);
        if (operators.size() >= numbers.size())
        {
            return calculate(operators, numbers);
        }
        else{
            System.out.println("Invalid Format");
        }
        return 0.0;

    }
}
