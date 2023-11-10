import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

public class MainFrame extends JFrame implements ActionListener {
    private final MapPanel mapPanel;


    public MainFrame() {
        var pane = getContentPane();
        pane.setPreferredSize(new Dimension(620, 410));
        var menu = new JMenu();
        var list = new ArrayList<JMenuItem>();
        var easy = new JMenuItem();
        easy.setText("5");
        easy.addActionListener(this);
        menu.add(easy);

        add(menu);
        setLayout(new GridLayout());
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        mapPanel = new MapPanel();

        add(mapPanel);
        pack();
        setVisible(true);
    }

    public void addMap(int[][] mapLayout, int n) {
        mapPanel.setSize(new Dimension(30 * n, 30 * n));
        mapPanel.setSize(n);
        mapPanel.n = n;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {

                var player = mapLayout[i][j];
                TileButton button;

                if(player == 0){
                    button = new EmptyTile();
                }
                else{
                    button = new PlayerTile(player);
                }

                mapPanel.addHolder(new TileHolder(button, j, i, mapPanel));
            }
        }
        mapPanel.handleSelection();
    }

    @Override
    public void actionPerformed(ActionEvent e) {

    }
}
