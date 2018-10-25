using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sdl.Tridion.Api.Client.ContentModel;
using Sdl.Tridion.Api.Client.Utils;

namespace Sdl.Tridion.Api.Client.Tests
{
    [TestClass]
    public class TestCMUri : TestClass
    {
        private static readonly string UriPrefix = "tcm:";
        private static readonly string UriSeparator = "-";

        private readonly int _testPublicationId = 33;
        private readonly int _testItemId = 3;
        private readonly int _testItemTypeId = 16;
        private readonly int _testVersion = 4;

        private string CreateCmUriString(ContentNamespace ns, int publicationId, int itemId, int itemTypeId, int version) 
            => $"{CmUri.NamespaceIdToIdentifer(ns)}:{publicationId}{UriSeparator}{itemId}{UriSeparator}{itemTypeId}{UriSeparator}v{version}";

        private string CreateCmUriString(ContentNamespace ns, int publicationId, int itemId, int itemTypeId)
            => $"{CmUri.NamespaceIdToIdentifer(ns)}:{publicationId}{UriSeparator}{itemId}{UriSeparator}{itemTypeId}";

        private string CreateCmUriString(ContentNamespace ns, int publicationId, int itemId)
            => $"{CmUri.NamespaceIdToIdentifer(ns)}:{publicationId}{UriSeparator}{itemId}";

        private void TestUri(CmUri testURI, ContentNamespace ns, int publicationId, int itemId, int itemTypeId)
        {
            Assert.AreEqual(ns, testURI.Namespace, "Namespace should match");
            Assert.AreEqual(publicationId, testURI.PublicationId, "Publication ID should match");
            Assert.AreEqual(itemId, testURI.ItemId, "Item ID should match");
            Assert.AreEqual(itemTypeId, (int)testURI.ItemType, "Item Type ID should match");
        }

        private void TestUri(CmUri testURI, ContentNamespace ns, int publicationId, int itemId, int itemTypeId, int version)
        {
            Assert.AreEqual(ns, testURI.Namespace, "Namespace should match");
            Assert.AreEqual(publicationId, testURI.PublicationId, "Publication ID should match");
            Assert.AreEqual(itemId, testURI.ItemId, "Item ID should match");
            Assert.AreEqual(itemTypeId, (int)testURI.ItemType, "Item Type ID should match");
            Assert.AreEqual(version, testURI.Version, "Versions should match");
        }

        [TestMethod]
        public void TestCreateFromString()
        {
            string uriString = CreateCmUriString(ContentNamespace.Docs, _testPublicationId, _testItemId, _testItemTypeId);
            try
            {
                CmUri testURI = new CmUri(uriString);
                TestUri(testURI, ContentNamespace.Docs, _testPublicationId, _testItemId, _testItemTypeId);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to parse {uriString} : {ex}");
            }
        }

        [TestMethod]
        public void TestCreateFromStringNoItemType()
        {
            string uriString = CreateCmUriString(ContentNamespace.Sites, _testPublicationId, _testItemId);
            try
            {
                CmUri testURI = new CmUri(uriString);
                TestUri(testURI, ContentNamespace.Sites, _testPublicationId, _testItemId, (int)ItemType.Component);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to parse {uriString} : {ex}");
            }
        }

        [TestMethod]
        public void TestCreateFromStringWithVersion()
        {
            string uriString = CreateCmUriString(ContentNamespace.Sites, _testPublicationId, _testItemId, _testItemTypeId, _testVersion);
            try
            {
                CmUri testURI = new CmUri(uriString);
                TestUri(testURI, ContentNamespace.Sites, _testPublicationId, _testItemId, _testItemTypeId, _testVersion);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to parse {uriString} : {ex}");
            }
        }

        [TestMethod]
        public void TestCreateFromStringWithVersionButNoItemType()
        {
            string uriString =
                $"{UriPrefix}{_testPublicationId}{UriSeparator}{_testItemId}{UriSeparator}v{_testVersion}";
            try
            {
                CmUri testURI = new CmUri(uriString);
                TestUri(testURI, ContentNamespace.Sites, _testPublicationId, _testItemId, (int)ItemType.Component, _testVersion);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to parse {uriString} : {ex}");
            }
        }

        [TestMethod]
        public void TestInvalidShortURI()
        {
            string uri = "tcm";
            try
            {
                CmUri testURI = new CmUri(uri);
                Assert.Fail($"Should not be able to create a uri from {uri}");
            }
            catch (Exception)
            {
                //expected behaviour
            }
        }

        [TestMethod]
        public void TestInvalidNamespace()
        {
            string uri = "tcr:1-2-3-4";
            try
            {
                CmUri testURI = new CmUri(uri);
                Assert.Fail($"Should not be able to create a uri from {uri}");
            }
            catch (Exception)
            {
                //expected behaviour
            }
        }

        [TestMethod]
        public void TestTcmUriOperatorOverloads()
        {
            CmUri testURI1 = new CmUri(ContentNamespace.Sites, 1, 2, ItemType.Category, 4);
            CmUri testURI2 = new CmUri(ContentNamespace.Sites, 1, 2, ItemType.Category, 4);

            Assert.AreEqual(testURI1.ToString(), testURI2.ToString(), "CmUri ToString() should be equal");
            Assert.AreEqual(testURI1.GetHashCode(), testURI2.GetHashCode(), "CmUri hashCode() should be equal");

            Assert.IsTrue(testURI1 == testURI2, "CmUri objects should be equal");
            Assert.IsTrue(testURI1.Equals(testURI2), "CmUri objects should be equal");

            HashSet<CmUri> testSet = new HashSet<CmUri> {testURI1};
            Assert.IsTrue(testSet.Contains(testURI1), "Due to object equality, set contains should match");
        }
    }
}
