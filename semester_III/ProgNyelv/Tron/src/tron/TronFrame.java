package tron;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.SQLException;
import java.util.ArrayList;

public class TronFrame extends JFrame implements ActionListener {

    private ArrayList<Player> players = new ArrayList<>();

    public TronFrame() {
        super("Tron");

        int width = 800;
        int height = 800;

        JMenuBar menuBar = new JMenuBar();
        setJMenuBar(menuBar);
        JMenu gameMenu = new JMenu("Game");
        menuBar.add(gameMenu);
        var customizeMenu = new JMenu("Customize");
        menuBar.add(customizeMenu);

        customizeMenu.add(customizationMenu("Player 1"));
        customizeMenu.add(customizationMenu("Player 2"));

        var quitMenu = new JMenuItem("Quit");
        quitMenu.addActionListener(this);
        gameMenu.add(quitMenu);

        var newMenu = new JMenuItem("New");
        newMenu.addActionListener(this);
        gameMenu.add(newMenu);
        var leaderboards = new JMenuItem("Leaderboards");
        leaderboards.addActionListener(this);
        gameMenu.add(leaderboards);

        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        players.add(new Player(100, 100, 10, 10, Color.RED, "Player 1"));
        players.add(new Player(200, 200, 10, 10, Color.BLUE, "Player 2"));

        GameEngine gameArea = null;
        try {
            gameArea = new GameEngine(width, height, players);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        getContentPane().add(gameArea);
        setSize(new Dimension(width, height));
        setResizable(false);
        setVisible(true);
    }

    private JMenu customizationMenu(String name){
        var playerMenu = new JMenu(name);
        var customizeColor = new JMenuItem(name + " Color");
        customizeColor.addActionListener(this);
        playerMenu.add(customizeColor);
        var customizeName = new JMenuItem(name + " Name");
        customizeName.addActionListener(this);
        playerMenu.add(customizeName);
        return playerMenu;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if(!(e.getSource() instanceof JMenuItem item)){
            return;
        }

        switch (item.getText()) {
            case "New":
                break;
            case "Leaderboards":
                var leaderboards = new LeaderboardFrame();
                break;

            case "Player 1 Color":
                Color newColorP1 = JColorChooser.showDialog(null, "Choose a color", Color.RED);
                players.getFirst().setColor(newColorP1);
                break;

            case "Player 1 Name":
                String p1Name = JOptionPane.showInputDialog("New name for " + players.getFirst());
                players.getFirst().setName(p1Name);
                break;

            case "Player 2 Color":
                Color newColorP2 = JColorChooser.showDialog(null, "Choose a color", Color.RED);
                players.getLast().setColor(newColorP2);
                break;

            case "Player 2 Name":
                String p2Name = JOptionPane.showInputDialog("New name for " + players.getLast());
                players.getLast().setName(p2Name);
                break;

            case "Quit":
                System.exit(0);
                break;
        }
    }

}
