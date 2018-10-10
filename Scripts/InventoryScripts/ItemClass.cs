using System;

[Serializable]
public class ItemClass {

    private string name;
    private int quantity;
    
    public ItemClass() { }

    public ItemClass(string Name, int Quantity)
    {
        name = Name;
        quantity = Quantity;
    }

    //getters and setters
    public string GetName() { return name; }
    public int GetQuantity() { return quantity; }
}
