package tron;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.sql.SQLException;

public class GameEngine extends JComponent {
    private final int FPS = 240;
    private boolean paused = false;
    private int height;
    private int width;

    private Timer newFrameTimer;

    private Player player1;
    private Player player2;

    private HighScores highScores;

    public GameEngine(int width, int height) throws SQLException {
        super();
        this.height = height;
        this.width = width;
        this.setPreferredSize(new Dimension(width, height));


        setupPlayerInput();
        this.getInputMap().put(KeyStroke.getKeyStroke("ESCAPE"), "escape");
        this.getActionMap().put("escape", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                paused = !paused;
            }
        });
        restart();

        newFrameTimer = new Timer(1000 / FPS, new NewFrameListener(this));
        newFrameTimer.start();

        highScores = new HighScores();
    }

    public void restart() {
        player1 = new Player(20, 20, 10, 10, Color.RED, "Player 1");
        player2 = new Player(width - 20, height - 20, 10, 10, Color.BLUE, "Player 2");
        player2.setDirection(DirectionEnum.UP);
        JOptionPane.showMessageDialog(this, "Get Ready!");
    }

    @Override
    protected void paintComponent(Graphics graphics) {
        super.paintComponent(graphics);

        var g2d = (Graphics2D) graphics;

        RenderingHints rh = new RenderingHints(
                RenderingHints.KEY_ANTIALIASING,
                RenderingHints.VALUE_ANTIALIAS_ON);
        g2d.setRenderingHints(rh);

        g2d.setColor(Color.BLACK);
        g2d.fillRect(0, 0, width, height);
        player1.paintComponent(g2d);
        player2.paintComponent(g2d);
    }

    private void setupPlayerInput(){
        this.getInputMap().put(KeyStroke.getKeyStroke("LEFT"), "pressed left");
        this.getActionMap().put("pressed left", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player1.setDirection(DirectionEnum.LEFT);
            }
        });

        this.getInputMap().put(KeyStroke.getKeyStroke("RIGHT"), "pressed right");
        this.getActionMap().put("pressed right", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player1.setDirection(DirectionEnum.RIGHT);
            }
        });
        this.getInputMap().put(KeyStroke.getKeyStroke("DOWN"), "pressed down");
        this.getActionMap().put("pressed down", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player1.setDirection(DirectionEnum.DOWN);
            }
        });

        this.getInputMap().put(KeyStroke.getKeyStroke("UP"), "pressed up");
        this.getActionMap().put("pressed up", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player1.setDirection(DirectionEnum.UP);
            }
        });


        this.getInputMap().put(KeyStroke.getKeyStroke("A"), "pressed a");
        this.getActionMap().put("pressed a", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player2.setDirection(DirectionEnum.LEFT);
            }
        });

        this.getInputMap().put(KeyStroke.getKeyStroke("D"), "pressed d");
        this.getActionMap().put("pressed d", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player2.setDirection(DirectionEnum.RIGHT);
            }
        });

        this.getInputMap().put(KeyStroke.getKeyStroke("S"), "pressed s");
        this.getActionMap().put("pressed s", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player2.setDirection(DirectionEnum.DOWN);
            }
        });

        this.getInputMap().put(KeyStroke.getKeyStroke("W"), "pressed w");
        this.getActionMap().put("pressed w", new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent ae) {
                player2.setDirection(DirectionEnum.UP);
            }
        });
    }

    private void validatePlayerMove(Player player){
        var isEnded = false;
        if(player.position.getX() + player.getWidth() >= width) {
            JOptionPane.showMessageDialog(this, player.getName() + " has lost!");
            isEnded = true;
        }

        if(player.position.getX() <= 0) {
            JOptionPane.showMessageDialog(this, player.getName() + " has lost!");
            isEnded = true;
        }

        if(player.position.getY() + player.getHeight() >= height) {
            JOptionPane.showMessageDialog(this, player.getName() + " has lost!");
            isEnded = true;
        }

        if(player.position.getY() <= 0) {
            JOptionPane.showMessageDialog(this, player.getName() + " has lost!");
            isEnded = true;
        }

        if(isEnded){
            restart();
        }
    }

    private void updateHighScores(Player player){
        var playerScore = 0;
        try {
            playerScore = highScores.getScore(player.getName());
        } catch (SQLException ignored) {}

        try {
            highScores.putHighScore(player.getName(), playerScore + 1);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }


    class NewFrameListener implements ActionListener {
        private JComponent parent;
        public NewFrameListener(JComponent parent){
            this.parent = parent;
        }

        @Override
        public void actionPerformed(ActionEvent ae) {
            if (!paused) {
                repaint();
                player1.move();
                player2.move();

                validatePlayerMove(player1);
                validatePlayerMove(player2);

                if(player1.checkCollision(player2)){
                    JOptionPane.showMessageDialog(parent, player2.getName() + " has lost!");
                    updateHighScores(player1);
                    restart();
                }

                if(player2.checkCollision(player1)){
                    JOptionPane.showMessageDialog(parent, player1.getName() + " has lost!");
                    updateHighScores(player2);
                    restart();
                }
            }
        }
    }
}
