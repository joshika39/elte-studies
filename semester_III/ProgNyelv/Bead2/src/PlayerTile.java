import javax.swing.*;
import java.awt.*;

public class PlayerTile extends TileButton {
    public final int id;

    public PlayerTile(int id) {
        this.id = id;
        switch (id){
            case 1:
                setBackground(Color.BLUE);
                break;
            case 2:
                setBackground(Color.GREEN);
        }
    }

    @Override
    public void setActive(boolean enabled) {
        setEnabled(enabled);

        switch (id) {
            case 1:
                setBackground(enabled ? new Color(83, 151, 224) : new Color(1, 50, 140));
                break;
            case 2:
                setBackground(enabled ? new Color(86, 224, 83) : new Color(3, 94, 1));

        }
    }
}
