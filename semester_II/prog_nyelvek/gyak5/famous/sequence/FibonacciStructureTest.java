package famous.sequence;
import static check.CheckThat.Condition.*;
import org.junit.jupiter.api.Test;
// import org.junit.jupiter.api.BeforeAll;
import check.CheckThat;

public class FibonacciStructureTest {
    // @BeforeAll
    // public static void beforeAll() {
    //     CheckThat.setLang("HU");
    // }

	@Test
    public void methodFib() {
        CheckThat.theClass("famous.sequence.Fibonacci")
            .hasMethodWithParams("fib", int.class)
                .thatIs(FULLY_IMPLEMENTED, USABLE_WITHOUT_INSTANCE, VISIBLE_TO_ALL)
                .thatReturns(int.class);
    }
}
