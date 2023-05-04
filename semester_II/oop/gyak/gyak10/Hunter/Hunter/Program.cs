    // See https://aka.ms/new-console-template for more information
    using Hunter.Factory;
    var vFactory = new VFurnitureFactory();
    var vChair = vFactory.CreateChair();
    
    vChair.SitOn();