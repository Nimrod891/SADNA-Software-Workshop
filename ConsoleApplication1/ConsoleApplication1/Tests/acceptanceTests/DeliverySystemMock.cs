// using System;
//
// namespace acceptanceTests{
//
// public class DeliverySystemMock : DeliverySystem {
//
// //    private Collection<String> deliveries = new LinkedList<>();
// //    private String address; //last address delivered to
//
//     private HashMap<String, LinkedList<String>> deliveries = new HashMap<>();
//
//
//     public HashMap<String, LinkedList<String>> getDeliveries() {
//         return deliveries;
//     }
//
//     public override boolean deliver(DeliveryData data) {
//         try {
//             if(!deliveries.keySet().contains(data.getUsername()))
//                 deliveries.put(data.getUsername(), new LinkedList<>());
//             deliveries.get(data.getUsername()).add(data.getAddress());
//             return true;
//         }
//         catch (Exception ex){
//             Console.WriteLine(ex.ToString());
//         }
//         
//     }
// }
// }