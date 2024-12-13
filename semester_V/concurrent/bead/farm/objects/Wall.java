package farm.objects;

import farm.Farm;

import static farm.Farm.ANSI_GREEN;

public class Wall {
    private final Farm farm;

    public Wall(Farm farm) {
        this.farm = farm;
    }

    @Override
    public String toString() {
        if (farm.isRunning()) {
            return "#";
        }
        return ANSI_GREEN + "#" + Farm.ANSI_RESET;
    }
}
