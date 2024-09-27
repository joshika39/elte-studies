package test.gy3;

import main.gy3.ThreadSafeMutableIntArray;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ThreadSafeMutableArrayTests {
    @Test
    public void givenThreadSafeMutableArray_whenSet_thenGet() {
        ThreadSafeMutableIntArray tsma = new ThreadSafeMutableIntArray(10);

        new Thread(() -> {
            for (int i = 0; i < 10; i++) {
                tsma.set(i, i);
            }
        }).start();

        new Thread(() -> {
            for (int i = 0; i < 10; i++) {
                tsma.set(i, i);
            }
        }).start();

        Assertions.assertAll(
                () -> Assertions.assertEquals(0, tsma.get(0)),
                () -> Assertions.assertEquals(1, tsma.get(1)),
                () -> Assertions.assertEquals(2, tsma.get(2)),
                () -> Assertions.assertEquals(3, tsma.get(3)),
                () -> Assertions.assertEquals(4, tsma.get(4)),
                () -> Assertions.assertEquals(5, tsma.get(5)),
                () -> Assertions.assertEquals(6, tsma.get(6)),
                () -> Assertions.assertEquals(7, tsma.get(7)),
                () -> Assertions.assertEquals(8, tsma.get(8)),
                () -> Assertions.assertEquals(9, tsma.get(9))
        );
    }
}
