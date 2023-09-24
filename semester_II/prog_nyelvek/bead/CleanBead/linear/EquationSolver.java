package linear;

import linear.algebra.*;

public class EquationSolver {
    public static void main(String[] args){
        if (args.length == 0){
            System.out.println("Nincsenek parametetek");
            return;
        }
        GaussianElimination ge = new GaussianElimination(GaussianElimination.stringsToDoubles(args));
        ge.print();
        ge.rowEchelonForm();
        ge.print();
        ge.backSubstitution();
        ge.print();
    }
}
