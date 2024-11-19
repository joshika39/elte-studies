import java.util.Random;

public class FarmSimulation {

    public static void main(String[] args) {
        Farm farm = new Farm(14, 14);
        farm.startSimulation();
    }
}

class Farm {
    private final Object[][] grid;
    private final int width;
    private final int height;
    private boolean simulationRunning = true;

    public Farm(int width, int height) {
        this.width = width;
        this.height = height;
        this.grid = new Object[height][width];
        initializeFarm();
    }

    private void initializeFarm() {
        // Falak létrehozása
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                if (i == 0 || i == height - 1 || j == 0 || j == width - 1) {
                    grid[i][j] = new Wall();
                } else {
                    grid[i][j] = new Empty();
                }
            }
        }

        // Kapuk elhelyezése
        placeGates();

        // Juhok és kutyák elhelyezése
        placeSheepAndDogs(10, 5);
    }

    private void placeGates() {
        Random random = new Random();
        for (int i = 0; i < 4; i++) {
            int position;
            if (i % 2 == 0) { // Felső vagy alsó fal
                position = random.nextInt(width - 2) + 1;
                grid[i == 0 ? 0 : height - 1][position] = new Gate();
            } else { // Bal vagy jobb fal
                position = random.nextInt(height - 2) + 1;
                grid[position][i == 1 ? 0 : width - 1] = new Gate();
            }
        }
    }

    private void placeSheepAndDogs(int sheepCount, int dogCount) {
        Random random = new Random();
        for (int i = 0; i < sheepCount; i++) {
            int x, y;
            do {
                x = height / 3 + random.nextInt(height / 3);
                y = width / 3 + random.nextInt(width / 3);
            } while (!(grid[x][y] instanceof Empty));
            Sheep sheep = new Sheep(x, y, this);
            grid[x][y] = sheep;
            sheep.start();
        }

        for (int i = 0; i < dogCount; i++) {
            int x, y;
            do {
                x = random.nextInt(height);
                y = random.nextInt(width);
            } while (!(grid[x][y] instanceof Empty));
            Dog dog = new Dog(x, y, this);
            grid[x][y] = dog;
            dog.start();
        }
    }

    public synchronized void moveObject(int oldX, int oldY, int newX, int newY) {
        if (grid[newX][newY] instanceof Empty || grid[newX][newY] instanceof Gate) {
            grid[newX][newY] = grid[oldX][oldY];
            grid[oldX][oldY] = new Empty();
            if (grid[newX][newY] instanceof Sheep && grid[newX][newY] instanceof Gate) {
                simulationRunning = false;
                System.out.println("Egy juh megszökött a farmról!");
            }
        }
    }

    public synchronized boolean isRunning() {
        return simulationRunning;
    }

    public synchronized void displayFarm() {
        System.out.print("\033[H\033[2J"); // Clear screen
        for (Object[] row : grid) {
            for (Object cell : row) {
                System.out.print(cell.toString());
            }
            System.out.println();
        }
    }

    public void startSimulation() {
        while (simulationRunning) {
            displayFarm();
            try {
                Thread.sleep(200);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }
}

class Sheep extends Thread {
    private int x, y;
    private final Farm farm;

    public Sheep(int x, int y, Farm farm) {
        this.x = x;
        this.y = y;
        this.farm = farm;
    }

    @Override
    public void run() {
        Random random = new Random();
        while (farm.isRunning()) {
            int dx = random.nextInt(3) - 1; // -1, 0, 1
            int dy = random.nextInt(3) - 1; // -1, 0, 1
            if (dx != 0 || dy != 0) {
                int newX = x + dx;
                int newY = y + dy;
                farm.moveObject(x, y, newX, newY);
                x = newX;
                y = newY;
            }
            try {
                Thread.sleep(200);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }

    @Override
    public String toString() {
        return "S";
    }
}

class Dog extends Thread {
    private int x, y;
    private final Farm farm;

    public Dog(int x, int y, Farm farm) {
        this.x = x;
        this.y = y;
        this.farm = farm;
    }

    @Override
    public void run() {
        Random random = new Random();
        while (farm.isRunning()) {
            int dx = random.nextInt(3) - 1; // -1, 0, 1
            int dy = random.nextInt(3) - 1; // -1, 0, 1
            if (dx != 0 || dy != 0) {
                int newX = x + dx;
                int newY = y + dy;
                farm.moveObject(x, y, newX, newY);
                x = newX;
                y = newY;
            }
            try {
                Thread.sleep(200);
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }

    @Override
    public String toString() {
        return "D";
    }
}

class Wall {
    @Override
    public String toString() {
        return "#";
    }
}

class Empty {
    @Override
    public String toString() {
        return " ";
    }
}

class Gate {
    @Override
    public String toString() {
        return "G";
    }
}