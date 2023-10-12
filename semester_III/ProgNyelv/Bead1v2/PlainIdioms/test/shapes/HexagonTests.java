/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/UnitTests/JUnit5TestClass.java to edit this template
 */
package shapes;

import org.junit.jupiter.api.*;

import static org.junit.jupiter.api.Assertions.*;

/**
 * @author JoshH
 */
public class HexagonTests {

    public HexagonTests() {
    }

    @BeforeAll
    public static void setUpClass() {
    }

    @AfterAll
    public static void tearDownClass() {
    }

    @BeforeEach
    public void setUp() {
    }

    @AfterEach
    public void tearDown() {
    }

    /**
     * Test of getLength method, of class Hexagon.
     */
    @Test
    public void HT_0001_Given_Hexagon_When_ConstructorCalledWithZeroLength_Then_ThrowsException() {
        System.out.println("getLength");
        var thrown = Assertions.assertThrows(IllegalArgumentException.class, () -> {
            new Hexagon(0, 0, 0);
        });

        Assertions.assertEquals("Length cannot be zero", thrown.getMessage());
    }

    /**
     * Test of getMethod method, of class Hexagon.
     */
    @Test
    public void HT_0002_Given_Hexagon_When_getLengthCalledCalled_Then_ReturnsLength() {
        System.out.println("getLength");
        Shape instance = new Hexagon(0, 0, 2);
        double expResult = 2;
        double result = instance.getLength();
        assertEquals(expResult, result, 0);
    }

    /**
     * Test of calculateArea method, of class Hexagon.
     */
    @Test
    public void HT_0003_Given_Hexagon_When_calculateAreaCalled_Then_ReturnsCorrectOverlappingArea() {
        System.out.println("calculateArea");
        Shape instance = new Hexagon(0, 0, 2);
        double expResult = 16;
        double result = instance.calculateArea();
        assertEquals(expResult, result, 0);
    }

}
