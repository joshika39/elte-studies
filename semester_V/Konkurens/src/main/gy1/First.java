package main.gy1;

public class First {
    public First() {
        Thread t1 = new Thread(() -> {
            for (int i = 0; i < 100_000; i++) {
                System.out.print(i + ", ");
            }
        });
        Thread t2 = new AlternateThread();

        t1.start();
        t2.start();

        try {
            t1.join();
            t2.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}

class AlternateThread extends Thread {
    @Override
    public void run() {
        for (int i = 0; i < 100_000; i++) {
            System.out.print(i + ", " + -i + ", ");
        }
    }
}
