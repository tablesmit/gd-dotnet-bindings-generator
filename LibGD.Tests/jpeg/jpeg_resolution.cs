using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_resolution
{
    [Test]
    public unsafe void TestJpeg_resolution()
	{
        int size = 0;

        gdImageStruct im = gd.gdImageCreate(100, 100);
		gd.gdImageSetResolution(im, 72, 300);
		int red = gd.gdImageColorAllocate(im, 0xFF, 0x00, 0x00);
		gd.gdImageFilledRectangle(im, 0, 0, 99, 99, red);
		IntPtr data = gd.gdImageJpegPtr(im, &size, 10);
		gd.gdImageDestroy(im);

		im = gd.gdImageCreateFromJpegPtr(size, data);
		gd.gdFree(data);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", im.res_x == 72 ? 1 : 0) == 0 ||
            GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", im.res_y == 300 ? 1 : 0) == 0)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "failed image resolution X (%d != 72) or Y (%d != 300)\n", im.res_x, im.res_y);
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
		gd.gdImageDestroy(im);
	}

    [Test]
    public unsafe void TestJpeg_resolutionCpp()
    {
        int size = 0;

        using (var image = new Image(100, 100))
        {
            image.SetResolution(72, 300);
            int red = image.ColorAllocate(0xFF, 0x00, 0x00);
            image.FilledRectangle(0, 0, 99, 99, red);
            IntPtr data = image.Jpeg(&size, 10);

            image.CreateFromJpeg(size, data);
            gd.gdFree(data);
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", image.ResX() == 72 ? 1 : 0) == 0 ||
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", image.ResY() == 300 ? 1 : 0) == 0)
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "failed image resolution X (%d != 72) or Y (%d != 300)\n", image.ResX(), image.ResY());
                Assert.Fail();
            }
        }
    }
}

