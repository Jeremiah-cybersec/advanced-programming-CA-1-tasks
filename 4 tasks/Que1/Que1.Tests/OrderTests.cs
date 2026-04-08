using Xunit;
using ChurrosApp;

namespace Que1.Tests
{
    /// <summary>
    /// Unit tests for the Order.PayBill() method.
    /// </summary>
    public class OrderTests
    {
        // Reset the static counter before each test so order numbers are predictable
        public OrderTests()
        {
            Order.ResetCounter(1);
        }

        [Fact]
        public void PayBill_SingleItem_ReturnsCorrectTotal()
        {
            // Arrange: 1x Churros with chocolate sauce @ €8.00
            var order = new Order("Churros with chocolate sauce", 1, 8.00);

            // Act
            double result = order.PayBill();

            // Assert
            Assert.Equal(8.00, result, precision: 2);
        }

        [Fact]
        public void PayBill_MultipleQuantity_ReturnsCorrectTotal()
        {
            // Arrange: 3x Churros with plain sugar @ €6.00 each = €18.00
            var order = new Order("Churros with plain sugar", 3, 6.00);

            // Act
            double result = order.PayBill();

            // Assert
            Assert.Equal(18.00, result, precision: 2);
        }

        [Fact]
        public void PayBill_NutellaItem_ReturnsCorrectTotal()
        {
            // Arrange: 2x Churros with Nutella @ €8.00 each = €16.00
            var order = new Order("Churros with Nutella", 2, 8.00);

            // Act
            double result = order.PayBill();

            // Assert
            Assert.Equal(16.00, result, precision: 2);
        }

        [Fact]
        public void PayBill_CinnamonSugar_ReturnsCorrectTotal()
        {
            // Arrange: 4x Churros with cinnamon sugar @ €6.00 each = €24.00
            var order = new Order("Churros with cinnamon sugar", 4, 6.00);

            // Act
            double result = order.PayBill();

            // Assert
            Assert.Equal(24.00, result, precision: 2);
        }

        [Fact]
        public void Order_InvalidQuantity_ThrowsArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Order("Churros with plain sugar", 0, 6.00));
        }
    }
}
