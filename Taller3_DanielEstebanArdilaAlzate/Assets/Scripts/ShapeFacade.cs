public class ShapeFacade
{
    private IShapeFactory currentFactory;

    public void SetFactory(IShapeFactory factory)
    {
        currentFactory = factory;
    }

    public UnityEngine.GameObject CreateShape()
    {
        return currentFactory?.CreateShape();
    }
}
