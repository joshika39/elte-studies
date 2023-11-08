import java.awt.*;

public class EmptyTile extends TileButton {
    public EmptyTile(){
        setBackground(Color.GRAY);
    }

    @Override
    public void setActive(boolean enabled) {
        setEnabled(enabled);
        setBackground(enabled ? new Color(149, 154, 163) : new Color(74, 75, 79));
    }

    @Override
    public String displayedName() {
        return "Empty";
    }
}
