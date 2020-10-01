﻿using libudev.Rules;
using libudev.Rules.Names;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.udev
{
    internal static class RuleCreator
    {
        public static Rule CreateAccessRule(string module, string mode)
        {
            return new Rule(
                new Token("KERNEL", Operator.Equal, module),
                new Token("MODE", Operator.Assign, mode)
            );
        }

        public static Rule CreateAccessRule(TabletProperties tablet, string subsystem, string mode)
        {
            return new Rule(
                new Token("SUBSYSTEM", Operator.Equal, subsystem),
                new ATTRS("idVendor", Operator.Equal, tablet.DigitizerIdentifier.VendorID.ToHexFormat()),
                new ATTRS("idProduct", Operator.Equal, tablet.DigitizerIdentifier.ProductID.ToHexFormat()),
                new Token("MODE", Operator.Assign, mode)
            );
        }

        public static Rule CreateOverrideRule(TabletProperties tablet)
        {
            return new Rule(
                new Token("SUBSYSTEM", Operator.Equal, "input"),
                new ATTRS("idVendor", Operator.Equal, tablet.DigitizerIdentifier.VendorID.ToHexFormat()),
                new ATTRS("idProduct", Operator.Equal, tablet.DigitizerIdentifier.ProductID.ToHexFormat()),
                new ENV("LIBINPUT_IGNORE_DEVICE", Operator.Assign, "1")
            );
        }
    }
}