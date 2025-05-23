using System.Text.Json.Serialization;

public class AzureVisionResponse
{
    [JsonPropertyName("objectsResult")]
    public ObjectsResult ObjectsResult { get; set; }

    [JsonPropertyName("modelVersion")]
    public string ModelVersion { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; }
}

public class ObjectsResult
{
    [JsonPropertyName("values")]
    public List<DetectedObject> Values { get; set; }
}

public class DetectedObject
{
    [JsonPropertyName("boundingBox")]
    public BoundingBox BoundingBox { get; set; }

    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; }
}

public class BoundingBox
{
    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }

    [JsonPropertyName("w")]
    public int Width { get; set; }

    [JsonPropertyName("h")]
    public int Height { get; set; }
}

public class Tag
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }
}

public class Metadata
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}
