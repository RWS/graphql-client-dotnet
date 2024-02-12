RWS Dynamic Experience Delivery GraphQL client for .NET
===
- Develop: ![Build Status](https://github.com/rws/graphql-client-dotnet/actions/workflows/workflow.yml/badge.svg?branch=develop)
- 2.2: ![Build Status](https://github.com/rws/graphql-client-dotnet/actions/workflows/workflow.yml/badge.svg?branch=release/2.2)

Prerequisites
-------------
For building .NET you must have the following installed:
- Visual Studio 2019
- .NET Framework 4.8

About
-----
The GraphQL client provides a public content API for .NET in order to retrieve content from the new GraphQL endpoint exposed by the Content Service.

We provide a strongly typed model built directly from the GraphQL schema used by the Content Service and also allow execution of general GraphQL requests.

You can generate a strongly typed model for any GraphQL schema by using the tools provided.


Support
-------
At RWS we take your investment in Digital Experience very seriously, if you encounter any issues with the Digital Experience Accelerator, please use one of the following channels:

- Report issues directly in [this repository](https://github.com/rws/graphql-client-dotnet/issues)
- Ask questions 24/7 on the RWS Tridion Community at https://tridion.stackexchange.com
- Contact RWS Professional Services for DXA release management support packages to accelerate your support requirements https://www.rws.com/support

Documentation
-------------
Documentation can be found online in the RWS documentation portal: https://docs.rws.com/LiveContent/content/en-US/SDL%20Tridion%20Sites-v1/GUID-7CE1DB91-B63C-4A5F-9307-CE1C6B3A8911


Branches and Contributions
--------------------------
We are using the following branching strategy:

 - `master` - Represents the latest stable version. This may be a pre-release version (tagged as `x.y Sprint z`). Updated each development Sprint (approximately bi-weekly).
 - `develop` - Represents the latest development version. Updated very frequently (typically nightly).
 - `release/x.y` - Represents the x.y Release. If hotfixes are applicable, they will be applied to the appropriate release branch so that the branch actually represents the initial release plus hotfixes.

All releases (including pre-releases and hotfix releases) are tagged. 

If you wish to submit a Pull Request, it should normally be submitted on the `develop` branch so that it can be incorporated in the upcoming release.

Fixes for severe/urgent issues (that qualify as hotfixes) should be submitted as Pull Requests on the appropriate release branch.

Always submit an issue for the problem, and indicate whether you think it qualifies as a hotfix. Pull Requests on release branches will only be accepted after agreement on the severity of the issue.
Furthermore, Pull Requests on release branches are expected to be extensively tested by the submitter.

Of course, it is also possible (and appreciated) to report an issue without associated Pull Requests.


License
-------
Copyright (c) 2014-2024 RWS Group.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and limitations under the License.
