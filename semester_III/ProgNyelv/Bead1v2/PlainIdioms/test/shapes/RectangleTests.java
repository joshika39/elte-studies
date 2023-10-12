package shapes;

import org.junit.jupiter.api.*;

import static org.junit.jupiter.api.Assertions.*;

/**
 *
 * @author JoshH
 */
public class RectangleTests {
    
    public RectangleTests() {
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
     * Test of getLength method, of class Rectangle.
     */
    @Test
    public void RT_0001_Given_Rectangle_When_ConstructorCalledWithLength_Then_ThrowsException() {
        System.out.println("getLength");
        var thrown = Assertions.assertThrows(IllegalArgumentException.class, () -> new Rectangle(0, 0, 0));

        Assertions.assertEquals("Length cannot be zero", thrown.getMessage());
    }

    /**
     * Test of getMethod method, of class Rectangle.
     */
    @Test
    public void RT_0002_Given_Rectangle_When_getLengthCalled_Then_ReturnsLength() {
        System.out.println("getLength");
        Shape instance = new Rectangle(0, 0, 2);
        double expResult = 2;
        double result = instance.getLength();
        assertEquals(expResult, result, 0);
    }

    /**
     * Test of calculateArea method, of class Rectangle.
     */
    @Test
    public void RT_0003_Given_Rectangle_When_calculateAreaCalled_Then_ReturnsCorrectOverlappingArea() {
        System.out.println("calculateArea");
        Shape instance = new Rectangle(0, 0, 3);
        double expResult = 9;
        double result = instance.calculateArea();
        assertEquals(expResult, result, 0);
    }
    
}
