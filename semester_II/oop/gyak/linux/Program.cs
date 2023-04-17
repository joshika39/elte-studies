using Implementation.Core;
using Implementation.Navigator;
using Infrastructure.IOManager;
using Infrastructure.Logger;
using Infrastructure.Navigator;
using BL;
using System;
using System.Collections.Generic;
using Unity;

var id = Guid.NewGuid();
var path = @"./log.txt";

var container = Bootsrapper.GetDefaultContainer(path, id);
var ioManager = container.Resolve<IIOManagerFactory>().CreateIOManager(container.Resolve<ILogger>());
ioManager.PrintSystemDetails("joshika39", "Joshua Hegedus", "YQMHWO");

var c1X = ioManager.ReadLine<double>(double.TryParse, "Enter the 1st complex numbers x: ", $"You have to enter a value with the type: {typeof(double)}");
var c1Y = ioManager.ReadLine<double>(double.TryParse, "Enter the 1st complex numbers y: ", $"You have to enter a value with the type: {typeof(double)}");
var c2X = ioManager.ReadLine<double>(double.TryParse, "Enter the 2st complex numbers x: ", $"You have to enter a value with the type: {typeof(double)}");
var c2Y = ioManager.ReadLine<double>(double.TryParse, "Enter the 2st complex numbers y: ", $"You have to enter a value with the type: {typeof(double)}");

var c1 = new Complex(c1X, c1Y);
var c2 = new Complex(c2X, c2Y);

var actions = new List<INavigatorElement>
{
    new NavigatorElement("Add", () => { ioManager.WriteLine($"{c1} + {c2} = {c1 + c2}");}),
    new NavigatorElement("Sub", () => { ioManager.WriteLine($"{c1} - {c2} = {c1 - c2}");}),
    new NavigatorElement("Mul", () => { ioManager.WriteLine($"{c1} * {c2} = {c1 * c2}");}),
    new NavigatorElement("Div", () => { ioManager.WriteLine($"{c1} / {c2} = {c1 / c2}");})
};

var navigator = container.Resolve<INavigator>();

_ = navigator.Show(actions);