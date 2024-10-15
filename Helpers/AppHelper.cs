public static class AppHelper
{
    public static Application CurrentApp
    {
        get
        {
            if (Application.Current == null)
            {
                // You can throw an exception, log an error, or handle it in another way
                throw new InvalidOperationException("Application.Current is not initialized.");
            }
            return Application.Current;
        }
    }
}