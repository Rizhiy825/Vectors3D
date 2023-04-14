using FluentAssertions;
using NUnit.Framework;
using Vectors3D;

namespace VectorsShould
{
    [TestFixture]
    public class VectorShould
    {
        [Test]
        public void SegmentsThatIntersect_FloatingPointerNumbers_ShouldReturnRightPoint()
        {
            // Значения подбирал предварительно смоделировав в SolidWorks)))
            var firstStart = new Vector3D(9.65, 6.7, 7.2);
            var firstEnd = new Vector3D(0, 3, 5);
            var secondStart = new Vector3D(5.9, 3.24283545, 7.45);
            var secondEnd = new Vector3D(0, 8.3, 2.1);
            var firstSegment = new Segment3D(firstStart, firstEnd);
            var secondSegment = new Segment3D(secondStart, secondEnd);
            
            var xExpected = 4.27224271;
            var yExpected = 4.63806197;
            var zExpected = 5.97398279;
            var precision = 1e-8d;

            var result = Segment3D.Intersect(firstSegment, secondSegment);

            result.X.Should().BeInRange(xExpected - precision, xExpected + precision);
            result.Y.Should().BeInRange(yExpected - precision, yExpected + precision);
            result.Z.Should().BeInRange(zExpected - precision, zExpected + precision);
        }

        [Test]
        public void SegmentsThatIntersect_IntegerNumbers_ShouldReturnRightPoint()
        {
            var firstStart = new Vector3D(10, 10, 3);
            var firstEnd = new Vector3D(0, 10, 5);
            var secondStart = new Vector3D(10, 10, 8);
            var secondEnd = new Vector3D(0, 10, 0);
            var firstSegment = new Segment3D(firstStart, firstEnd);
            var secondSegment = new Segment3D(secondStart, secondEnd);

            var expected = new Vector3D(5, 10, 4);

            var result = Segment3D.Intersect(firstSegment, secondSegment);

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void SegmentsDontIntersect_ButLinesDo_ShouldReturnNull()
        {
            var firstStart = new Vector3D(5, -5, 0);
            var firstEnd = new Vector3D(5, 5, 0);
            var secondStart = new Vector3D(0, 0, 0);
            var secondEnd = new Vector3D(2.5, 0, 0);
            var firstSegment = new Segment3D(firstStart, firstEnd);
            var secondSegment = new Segment3D(secondStart, secondEnd);

            var expected = new Vector3D(5, 10, 4);

            var result = Segment3D.Intersect(firstSegment, secondSegment);

            result.Should().Be(null);
        }

        [Test]
        public void SegmentsDontIntersect_ShouldReturnNull()
        {
            var firstStart = new Vector3D(10, 10, 3);
            var firstEnd = new Vector3D(0, 10, 0);
            var secondStart = new Vector3D(10, 10, 8);
            var secondEnd = new Vector3D(0, 3, 5);
            var firstSegment = new Segment3D(firstStart, firstEnd);
            var secondSegment = new Segment3D(secondStart, secondEnd);

            var expected = new Vector3D(5, 10, 4);

            var result = Segment3D.Intersect(firstSegment, secondSegment);
            
            result.Should().Be(null);
        }

        [Test]
        public void SameSegments_ShouldReturnNull()
        {
            var a = new Vector3D(0, 0, 0);
            var b = new Vector3D(1, 1, 1);
            var segment = new Segment3D(a, b);

            var result = Segment3D.Intersect(segment, segment);

            result.Should().Be(null);
        }

        [Test]
        public void ParallelVectors_ShouldReturnNull()
        {
            var firstStart = new Vector3D(0, 0, 0);
            var firstEnd = new Vector3D(0, 0, 10);
            var secondStart = new Vector3D(5, 0, 0);
            var secondEnd = new Vector3D(5, 0, 10);
            var firstSegment = new Segment3D(firstStart, firstEnd);
            var secondSegment = new Segment3D(secondStart, secondEnd);
            
            var result = Segment3D.Intersect(firstSegment, secondSegment);

            result.Should().Be(null);
        }

        [Test]
        public void CollinearVectors_ShouldReturnNull()
        {
            var firstStart = new Vector3D(0, 0, 0);
            var firstEnd = new Vector3D(0, 0, 10);
            var secondStart = new Vector3D(0, 0, 20);
            var secondEnd = new Vector3D(0, 0, 30);
            var firstSegment = new Segment3D(firstStart, firstEnd);
            var secondSegment = new Segment3D(secondStart, secondEnd);

            var result = Segment3D.Intersect(firstSegment, secondSegment);

            result.Should().Be(null);
        }

        [Test]
        public void NullArgs_ShouldThrowExeption()
        {
            Action act = () => Segment3D.Intersect(null, null);

            act.Should().Throw<ArgumentException>();
        }
    }
}