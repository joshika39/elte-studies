package linear;

import linear.algebra.*;

public class EquationSolver {
    public static void main(String[] args){
        GaussianElimination ge = new GaussianElimination(args);
        ge.print();
        ge.rowEchelonForm();
        ge.print();
        ge.backSubstitution();
        ge.print();
    }
}
