// using System;
//
// namespace acceptanceTests{
//
// public class PaymentSystemMock : PaymentSystem {
//
// //    private double amount; //last price purchased
// //    private boolean isSucceed;
// //    private Collection<String> payments = new LinkedList<>(); //list of usernames
//     private HashMap<String, LinkedList<Double>> payments = new HashMap<>();
//
// //    public void setSucceed(boolean succeed) {
// //        isSucceed = succeed;
// //    }
//
//
//     public HashMap<String, LinkedList<Double>> getPayments() {
//         return payments;
//     }
//
//     public override void payBack(PaymentData data) {
//
//     }
//
//     public override boolean pay(PaymentData data) {
//         try {
//               //        if(!isSucceed)
//             //            throw  new PaymentSystemException();
//             if(!payments.keySet().contains(data.getUsername()))
//                 payments.put(data.getUsername(), new LinkedList<>());
//             payments.get(data.getUsername()).add(data.getPaymentValue());
//
//             return true;
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//     }
// }
// }