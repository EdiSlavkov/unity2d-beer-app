using Newtonsoft.Json;
using System.Collections.Generic;

public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonProperty("tagline")]
    public string ShortDescription { get; set; }
    [JsonProperty("first_brewed")]
    public string FirstBrewed { get; set; }
    [JsonProperty("description")]
    public string FullDescription { get; set; }
    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }
    private float abv;
    public float? Abv 
    { 
        get { return abv; }
        set
        {
            if (value == null)
            {
                abv = 0f;
            }
            else
            {
                abv = (float)value;
            }
        }
    }
    private float ibu;
    public float? Ibu 
    {
        get { return ibu; }
        set
        {
            if (value == null)
            {
                ibu = 0f;
            }
            else
            {
                ibu = (float)value;
            }
        }
    }
    private float ebc;
    public float? Ebc 
    {
        get { return ebc; }
        set
        {
            if (value == null)
            {
                ebc = 0f;
            }
            else
            {
                ebc = (float)value;
            }
        }
    }
    public Ingredients Ingredients { get; set; }
    [JsonProperty("food_pairing")]
    public List<string> FoodPairing { get; set; }
    [JsonProperty("brewers_tips")]
    public string BrewersTips { get; set; }
    [JsonProperty("contributed_by")]
    public string ContributedBy { get; set; }
}

public abstract class Ingredient
{
    public string Name { get; set; }
    public IngredientAmount Amount { get; set; }
}

public class Malt : Ingredient
{
    public override string ToString()
    {
        return $"{Name} {Amount.Value} {Amount.Unit}";
    }
}

public class IngredientAmount
{
    public float Value { get; set; }
    public string Unit { get; set; }
}

public class Hop : Ingredient
{
    public string Add { get; set; }
    public string Attribute { get; set; }
    public override string ToString()
    {
        return $"{Name} {Amount.Value} {Amount.Unit}";
    }
}

public class Ingredients
{
    public List<Malt> Malt { get; set; }
    public List<Hop> Hops { get; set; }
    public string Yeast { get; set; }
}