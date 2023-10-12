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
public class CircleTests {

    public CircleTests() {
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
     * Test of getLength method, of class Circle.
     */
    @Test
    public void CT_0001_Given_Circle_When_ConstructorCalledWithZeroRadius_Then_ThrowsException() {
        System.out.println("getLength");
        var thrown = Assertions.assertThrows(IllegalArgumentException.class, () -> {
            Shape instance = new Circle(0, 0, 0);
        });

        Assertions.assertEquals("Radius cannot be zero", thrown.getMessage());
    }

    /**
     * Test of getMethod method, of class Circle.
     */
    @Test
    public void CT_0002_Given_Circle_When_getLengthCalled_Then_ReturnsRadius() {
        System.out.println("getLength");
        Circle instance = new Circle(0, 0, 2);
        double expResult = 2;
        double result = instance.getLength();
        assertEquals(expResult, result, 0);
    }

    /**
     * Test of calculateArea method, of class Circle.
     */
    @Test
    public void CT_0003_Given_Circle_When_calculateAreaCalled_Then_ReturnsCorrectOverlappingArea() {
        System.out.println("calculateArea");
        Circle instance = new Circle(0, 0, 3);
        double expResult = 36;
        double result = instance.calculateArea();
        assertEquals(expResult, result, 0);
    }

}
