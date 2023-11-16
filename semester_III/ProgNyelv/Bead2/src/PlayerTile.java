import javax.swing.*;
import javax.swing.border.LineBorder;
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
                setBackground(new Color(83, 151, 224));
                break;
            case 2:
                setBackground(new Color(86, 224, 83));
        }

        if(enabled){
            setBorder(new LineBorder(Color.BLACK, 2));
        } else {
            setBorder(null);
        }
    }

    @Override
    public String displayedName() {
        return "Player " + id;
    }
}
