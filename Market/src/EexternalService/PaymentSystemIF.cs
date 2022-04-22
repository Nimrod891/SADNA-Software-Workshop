namespace externalService;

public interface PaymentSystem {
    bool pay(PaymentData data);

    void payBack(PaymentData data);
}
