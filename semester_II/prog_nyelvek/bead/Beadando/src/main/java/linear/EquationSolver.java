package linear;

import linear.algebra.GaussianElimination;

public class EquationSolver {
    public static void main(String[] args){
        GaussianElimination ge = new GaussianElimination(3, args);
        ge.print();
        ge.rowEchelonForm();
        ge.print();
        ge.backSubstitution();
        ge.print();
    }
}
