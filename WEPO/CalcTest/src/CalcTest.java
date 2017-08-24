public class CalcTest
{
    public static void main(String[] args)
    {
        Calculator calc = new Calculator();

        String[] exp =  { "1", "2","/" };
        System.out.println(calc.evalRPN(exp));

    }
}
