package tron;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.SQLException;

public class TronFrame extends JFrame implements ActionListener {
    public TronFrame() {
        super("Tron");

        int width = 800;
        int height = 800;

        JMenuBar menuBar = new JMenuBar();
        setJMenuBar(menuBar);
        JMenu gameMenu = new JMenu("Game");
        menuBar.add(gameMenu);
        var newMenu = new JMenuItem("New");
        gameMenu.add(newMenu);
        var leaderboards = new JMenuItem("Leaderboards");
        gameMenu.add(leaderboards);

        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        GameEngine gameArea = null;
        try {
            gameArea = new GameEngine(width, height);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        getContentPane().add(gameArea);
        setSize(new Dimension(width, height));
        setResizable(false);
        setVisible(true);
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
                break;
        }
    }

}
