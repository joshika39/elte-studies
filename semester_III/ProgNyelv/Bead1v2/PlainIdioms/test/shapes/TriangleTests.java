/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/UnitTests/JUnit5TestClass.java to edit this template
 */
package shapes;

import org.junit.jupiter.api.*;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.text.DecimalFormat;

import static org.junit.jupiter.api.Assertions.*;

/**
 *
 * @author JoshH
 */
public class TriangleTests {
    private static final DecimalFormat dfZero = new DecimalFormat("0.00");

    public TriangleTests() {
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
     * Test of getLength method, of class Triangle.
     */
    @Test
    public void TT_0001_Given_Triangle_When_ConstructorCalledWithZeroLength_Then_ThrowsException() {
        System.out.println("getLength");
        var thrown = Assertions.assertThrows(IllegalArgumentException.class, () -> new Triangle(0, 0, 0));

        Assertions.assertEquals("Length cannot be zero", thrown.getMessage());
    }

    /**
     * Test of getMethod method, of class Triangle.
     */
    @Test
    public void TT_0002_Given_Triangle_When_getLengthCalled_Then_ReturnsLength() {
        System.out.println("getLength");
        Shape instance = new Triangle(0, 0, 2);
        double expResult = 2;
        double result = instance.getLength();
        assertEquals(expResult, Math.round(result), 0);
    }

    /**
     * Test of calculateArea method, of class Triangle.
     */
    @Test
    public void TT_0003_Given_Triangle_When_calculateAreaCalled_Then_ReturnsCorrectOverlappingArea() {
        System.out.println("calculateArea");
        Shape instance = new Triangle(0, 0, 5);
        String expResult = "18.75";
        double result = instance.calculateArea();
        assertEquals(expResult, dfZero.format(result));
    }
}
