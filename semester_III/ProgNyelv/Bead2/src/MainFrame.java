import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.Random;

public class MainFrame extends JFrame implements ActionListener {
    private MapPanel mapPanel;


    public MainFrame() {
        var pane = getContentPane();
        pane.setPreferredSize(new Dimension(620, 410));

        JMenuBar menuBar = new JMenuBar();
        setJMenuBar(menuBar);
        JMenu gameMenu = new JMenu("Game");
        menuBar.add(gameMenu);
        JMenu newMenu = new JMenu("New");
        gameMenu.add(newMenu);

        newMenu.add(getButton("3"));
        newMenu.add(getButton("4"));
        newMenu.add(getButton("6"));
        newMenu.add(getButton("10"));

        mapPanel = new MapPanel();

        setLayout(new GridLayout());
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        getContentPane().add(mapPanel, BorderLayout.CENTER);
        setIconImage(new ImageIcon("src/icon.png").getImage());

        pack();
        setVisible(true);
    }

    private JMenuItem getButton(String content) {
        var item = new JMenuItem();
        item.setText(content);
        item.addActionListener(this);
        return item;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if(!(e.getSource() instanceof JMenuItem item)){
            return;
        }

        var numText = item.getText();
        startNew(Integer.parseInt(numText));
    }

    private void startNew(int n){
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


        mapPanel.removeAll();

        mapPanel.setSize(new Dimension(30 * n, 30 * n));
        mapPanel.setSize(n);

        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {

                var player = layout[i][j];
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

        if(getContentPane().getComponentCount() > 1){
            getContentPane().remove(1);
        }
    }
}
