package linear;

import linear.algebra.GaussianElimination;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertArrayEquals;
import static org.junit.jupiter.api.Assertions.assertEquals;

public class EquationSolverTest {
    @Test
    public void mainTest() {
        EquationSolver.main(new String[]{"2","1","-1","8","-3","-1","2","-11","-2","1","2","-3"});
    }
}
