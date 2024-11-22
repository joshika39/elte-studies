package main.gy4;

/*
Öt csendes filozófus ül egy kerek asztalnál, az asztal közepén egy tál spagetti. Öt villa is van az asztalon, minden két filozófus között egy. Minden filozófus felváltva gondolkodik és eszik. Egy filozófus csak akkor tud enni, ha mindkét kezében villa van. Minden villát egyszerre csak egy filozófus használhat, tehátt csak akkor tud vele enni, ha más nem használja. Miután a filozófus befejezte az evést, visszateszi a villákat, hogy a szomszédai is tudják őket használni. A fiilozófus csak akkor tudja felvenni a bal vagy a jobb villáját, ha azok az asztalon vannak, és csak azokkal tudja megkezdeni az evést.
Ezt a jelenetet szimulálja a Philosophers osztály. Sajnos a szimuláció időnként holtpontba kerül, ha az összes filozófus egyszerre veszi fel a bal villáját, és vár a jobbra.
Javítsd ki a szimulációt úgy, hogy minden filozófus előbb az alacsonyabb sorszámú villát vegye el, és utána a magasabb sorszámút, így elkerülve a holtpontot!
 */

public class EatingPhilosophers {
    private static final int NUMBER_OF_PHILOSOPHERS = 5;
    private static final int THINK_TIME = 100;
    private static final int EAT_TIME = 50;

    private static Object[] forks = new Object[NUMBER_OF_PHILOSOPHERS];
    static {
        for (int i = 0; i < NUMBER_OF_PHILOSOPHERS; ++i) {
            forks[i] = new Object();
        }
    }

    private static class Philosopher extends Thread {
        private int id;

        Philosopher(int id) {
            this.id = id;
        }

        @Override
        public void run() {
            while (true) {
                think();
                eat();
            }
        }

        private void think() {
            System.err.println("#" + id + " Thinking...");
            try {
                Thread.sleep(THINK_TIME);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }

        private void eat() {
            System.err.println("#" + id + " Taking left fork.");
            synchronized (forks[Math.min(id, (id + 1) % NUMBER_OF_PHILOSOPHERS)]) {
                System.err.println("#" + id + " Taking right fork.");
                synchronized (forks[Math.max(id, (id + 1) % NUMBER_OF_PHILOSOPHERS)]) {
                    System.err.println("#" + id + " Eating...");
                    try {
                        Thread.sleep(EAT_TIME);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
    }

    public static void main(String[] args) {
        Philosopher[] philosophers = new Philosopher[NUMBER_OF_PHILOSOPHERS];

        for (int i = 0; i < NUMBER_OF_PHILOSOPHERS; ++i) {
            philosophers[i] = new Philosopher(i);
            philosophers[i].start();
        }
    }
}