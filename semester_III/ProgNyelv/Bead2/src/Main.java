import javax.swing.*;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Random;

public class Main {
    public static void main(String[] args) {

        var frame = new MainFrame();

        var n = 5;
        var layout = new int[n][n];
        var rand = new Random();

        for (int i = 0; i < n * 2; i++){
            var success = false;
            while (!success){
                var x = rand.nextInt(n);
                var y = rand.nextInt(n);
                var value = layout[y][x];

                if(value == 0){
                    layout[y][x] = i % 2 == 0 ? 1 : 2;
                    success = true;
                }
            }
        }

        frame.addMap(layout, n);
        System.out.println("Hello world!");
    }
}