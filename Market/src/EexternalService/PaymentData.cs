namespace externalService;
public class PaymentData {

    private double paymentValue;
    private string username;

    public string getUsername() {
        return username;
    }

    public double getPaymentValue() {
        return paymentValue;
    }

    public PaymentData(double paymentValue, string username) {
        this.paymentValue = paymentValue;
        this.username = username;
    }
}
