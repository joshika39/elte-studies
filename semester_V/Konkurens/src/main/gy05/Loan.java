package main.gy5;

import java.util.concurrent.*;

public class Loan {
    private static int POOL_SIZE = 4;
    private static int clientNumber = 10;
    private static int stepCount = 10000;
    private static int loanedAmount = 0;
    private static int[] clients = new int[clientNumber];
    private static Object lock = new Object();


    public static void main(String[] args) throws InterruptedException {
        Callable<Integer> callable = () -> {
            int loan = ThreadLocalRandom.current().nextInt(1000, 10000);
            synchronized (lock) {
                loanedAmount += loan;
            }
            return loan;
        };
        ExecutorService pool = Executors.newFixedThreadPool(POOL_SIZE);
        /*
        for (int i = 0; i < clientNumber; i++) {
            final int clientId = i;
            pool.submit(() -> {
                for (int j = 0; j < stepCount; j++) {
                    int loan = ThreadLocalRandom.current().nextInt(1000, 10000);
                    synchronized (lock) {
                        loanedAmount += loan;
                    }
                    clients[clientId] += loan;
                }
            });
        }

        pool.shutdown();
        pool.awaitTermination(1, java.util.concurrent.TimeUnit.MINUTES);
        System.out.println("Total loaned amount: " + loanedAmount);
        int total = 0;
        for (int i : clients) {total += i;}
        System.out.println("Is match: " + (total == loanedAmount));
         */
        Future<Integer>[] loans = new Future[clientNumber];
        for (int i = 0; i < clientNumber; i++) {
            loans[i] = pool.submit(callable);
        }

        for (int i = 0; i < clientNumber; i++) {
            try {
                clients[i] = loans[i].get();
            } catch (ExecutionException e) {
                e.printStackTrace();
            }
        }

        pool.shutdown();
        pool.awaitTermination(1, TimeUnit.MINUTES);
        System.out.println("Total loaned amount: " + loanedAmount);
        int total = 0;
        for (int i : clients) {total += i;}
        System.out.println("Is match: " + (total == loanedAmount));

    }
}
