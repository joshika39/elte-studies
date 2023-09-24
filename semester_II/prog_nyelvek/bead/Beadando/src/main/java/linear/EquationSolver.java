package linear;

import linear.algebra.GaussianElimination;

public class EquationSolver {
    public static void main(String[] args){
        GaussianElimination ge = new GaussianElimination("2,1,-1,8 -3,-1,2,-11 -2,1,2,-3");
        ge.print();
        ge.rowEchelonForm();
        ge.print();
        ge.backSubstitution();
        ge.print();
    }
}
