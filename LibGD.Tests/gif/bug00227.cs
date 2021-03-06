using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00227
{
    public const int WIDTH = 150;
    public const int HEIGHT = 1;
    public const int DELAY = 100;
    public const int PROBE_SIZE = 11;

    [Test]
    public void TestBug00227()
	{
        int i;

		/* generate a GIF animation */
		gdImageStruct im0 = gd.gdImageCreate(WIDTH, HEIGHT);
		if (im0 == null)
			Assert.Fail();
		for (i = 0; i < WIDTH; i++)
		{
			int c = gd.gdImageColorAllocate(im0, i, 0xff, 0xff);
			gd.gdImageSetPixel(im0, i, 0, c);
		}

		IntPtr fp = C.fopen("bug00227.gif", "wb");
		if (fp == null)
			Assert.Fail();

		gd.gdImageGifAnimBegin(im0, fp, 0, 0);

		gd.gdImageGifAnimAdd(im0, fp, 1, 0, 0, DELAY, 1, null);

		gdImageStruct im1 = gd.gdImageCreate(WIDTH, HEIGHT);
		if (im1 == null)
			Assert.Fail();
		for (i = 0; i < WIDTH; i++)
		{
			int c = gd.gdImageColorAllocate(im1, i, 0x00, 0xff);
			gd.gdImageSetPixel(im1, i, 0, c);
		}
		gd.gdImageGifAnimAdd(im1, fp, 1, 0, 0, DELAY, 1, im0);

		gdImageStruct im2 = gd.gdImageCreate(WIDTH, HEIGHT);
		if (im2 == null)
			Assert.Fail();
		for (i = 0; i < WIDTH; i++)
		{
			int c = gd.gdImageColorAllocate(im2, i, 0xff, 0x00);
			gd.gdImageSetPixel(im2, i, 0, c);
		}
		gd.gdImageGifAnimAdd(im2, fp, 1, 0, 0, DELAY, 1, im1);

		gd.gdImageGifAnimEnd(fp);

		C.fclose(fp);

		gd.gdImageDestroy(im0);
		gd.gdImageDestroy(im1);
		gd.gdImageDestroy(im2);

		/* check the Global Color Table flag */
        //fp = fopen("bug00227.gif", "rb");
        //if (fp == null)
        //    return 1;
        //buf = malloc(DefineConstants.PROBE_SIZE);
        //if (buf == 0)
        //    return 1;
        //if (DefineConstants.PROBE_SIZE != fread(buf, 1, DefineConstants.PROBE_SIZE, fp))
        //    return 1;
        //if (buf[DefineConstants.PROBE_SIZE-1] & 0x80)
        //    return 1;
        //free(buf);
        //fclose(fp);
        //return 0;
	}

    [Test]
    public void TestBug00227Cpp()
    {
        int i;

        /* generate a GIF animation */
        using (var image0 = new Image(WIDTH, HEIGHT))
        {
            if (!image0.good())
                Assert.Fail();
            for (i = 0; i < WIDTH; i++)
            {
                int c = image0.ColorAllocate(i, 0xff, 0xff);
                image0.SetPixel(i, 0, c);
            }

            IntPtr fp = C.fopen("bug00227.gif", "wb");
            if (fp == IntPtr.Zero)
                Assert.Fail();

            image0.GifAnimBegin(fp, 0, 0);

            image0.GifAnimAdd(fp, 1, 0, 0, DELAY, 1, (gdImageStruct) null);

            using (var image1 = new Image(WIDTH, HEIGHT))
            {
                if (!image1.good())
                    Assert.Fail();
                for (i = 0; i < WIDTH; i++)
                {
                    int c = image1.ColorAllocate(i, 0x00, 0xff);
                    image1.SetPixel(i, 0, c);
                }
                image1.GifAnimAdd(fp, 1, 0, 0, DELAY, 1, image0);

                using (var image2 = new Image(WIDTH, HEIGHT))
                {
                    if (!image2.good())
                        Assert.Fail();
                    for (i = 0; i < WIDTH; i++)
                    {
                        int c = image2.ColorAllocate(i, 0xff, 0x00);
                        image2.SetPixel(i, 0, c);
                    }
                    image2.GifAnimAdd(fp, 1, 0, 0, DELAY, 1, image1);

                    gd.gdImageGifAnimEnd(fp);

                    C.fclose(fp);
                }
            }
        }

        /* check the Global Color Table flag */
        //fp = fopen("bug00227.gif", "rb");
        //if (fp == null)
        //    return 1;
        //buf = malloc(DefineConstants.PROBE_SIZE);
        //if (buf == 0)
        //    return 1;
        //if (DefineConstants.PROBE_SIZE != fread(buf, 1, DefineConstants.PROBE_SIZE, fp))
        //    return 1;
        //if (buf[DefineConstants.PROBE_SIZE-1] & 0x80)
        //    return 1;
        //free(buf);
        //fclose(fp);
        //return 0;
    }
}

