package test.gy3;

import main.gy3.ThreadSafeMutableInteger;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ThreadSafeMutableIntegerTests {

    @Test
    public void givenThreadSafeMutableInteger_whenIncrement_thenGetAndIncrement() {
        ThreadSafeMutableInteger tsmi = new ThreadSafeMutableInteger();

        Thread t1 = new Thread(() -> {
            for (int i = 0; i < 100_000; i++) {
                tsmi.incrementAndGet();
            }
        });
        Thread t2 = new Thread(() -> {
            for (int i = 0; i < 100_000; i++) {
                tsmi.getAndIncrement();
            }
        });

        t1.start();
        t2.start();

        try {
            t1.join();
            t2.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        Assertions.assertEquals(200_000, tsmi.get());
    }

    @Test
    public void givenTSMI_whenIncrementAndDecrement_thenGet() {
        ThreadSafeMutableInteger tsmi = new ThreadSafeMutableInteger();

        Thread t1 = new Thread(() -> {
            for (int i = 0; i < 100_000; i++) {
                tsmi.incrementAndGet();
            }
        });
        Thread t2 = new Thread(() -> {
            for (int i = 0; i < 100_000; i++) {
                tsmi.decrementAndGet();
            }
        });

        t1.start();
        t2.start();

        try {
            t1.join();
            t2.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        Assertions.assertEquals(0, tsmi.get());
    }
}
