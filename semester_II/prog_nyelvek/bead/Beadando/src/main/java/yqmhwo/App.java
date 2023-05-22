package yqmhwo;

import linear.algebra.GaussianElimination;
import linear.algebra.RREF;

public class App {
    public static void main(String[] args) {

        double[][] matrix = {
                {2.0, 3.0, 5.0, 8.0},
                {1.0, 2.0, 3.0, 5.0},
                {4.0, 6.0, 8.0, 12.0}};

//        RREF.rref(matrix);
        GaussianElimination ge = new GaussianElimination(3, 4, matrix);
//
        ge.rowEchelonForm();
        ge.print();
//        ToReducedRowEchelonForm(matrix);
//        GaussianElimination.print(matrix);
    }


    public static void ToReducedRowEchelonForm(double[][] matrix) {
        int lead = 0;
        int rowCount = matrix.length;
        int columnCount = matrix[0].length;

        for (int r = 0; r < rowCount; r++) {
            if (columnCount <= lead) {
                break;
            }

            int i = r;
            while (matrix[i][lead] == 0) {
                i++;
                if (rowCount == i) {
                    i = r;
                    lead++;
                    if (columnCount == lead) {
                        break;
                    }
                }
            }

            // Swap rows i and r
            double[] temp = matrix[i];
            matrix[i] = matrix[r];
            matrix[r] = temp;

            // Divide row r by M[r, lead] if it's not zero
            if (matrix[r][lead] != 0) {
                double divisor = matrix[r][lead];
                for (int j = 0; j < columnCount; j++) {
                    matrix[r][j] /= divisor;
                }
            }

            for (i = 0; i < rowCount; i++) {
                if (i != r) {
                    double factor = matrix[i][lead];
                    for (int j = 0; j < columnCount; j++) {
                        matrix[i][j] -= factor * matrix[r][j];
                    }
                }
            }

            lead++;
        }
    }
}
