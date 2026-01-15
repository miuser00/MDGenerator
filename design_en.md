# MDGenerator Product Design Document

> [中文版本 (Chinese Version)](./design.md)

![](images/20221218004327.png)

## Product Overview

**MDGenerator** is a powerful Markdown editor that integrates AI-assisted writing, WordPress auto-publishing, image management, and other features, aiming to provide users with an efficient and convenient document creation experience.

**Current Version:** V0.9

## Design Principles

EditorMD is a very convenient Markdown editor framework based on HTML+JS. We wrap and automate it through the Windows built-in Webbrowser control, encapsulating this editor framework for the WinForm platform. The process involves the following steps:

1. Deeply customize the IE browser built into the .NET platform according to the minimum runtime environment requirements of Webbrowser, making it compatible with Windows 7, Windows 10, and Windows 11.
2. Write adapter code in both the JS layer and C# layer to complete basic data exchange between C# code and EditorMD, implementing remote control of the Web framework from WinForm desktop program code.
3. Write adapters to wrap functions that need to interact with EditorMD, such as extracting image information, exchanging image link addresses, and synchronizing data between the code area and preview area.
4. Integrate WordPress REST API to implement automatic article and image upload and publishing.
5. Integrate OpenAI-compatible AI Agent interface to provide intelligent writing assistance.

## Code Design

MDGenerator is based on the widely-used open-source MD framework EditorMD and has been further developed. The program is divided into a user UI layer and a framework adapter layer, which are essentially separated. The program code strives to implement the required functions with minimal code while maintaining good extensibility.

### Core Functional Modules

1. **Markdown Editing Core**
   - Dual-pane editing and real-time preview based on EditorMD
   - Supports all MD standard syntax and extended syntax
   - Automatic clipboard image solidification

2. **AI-Assisted Writing Module** (V0.9 New)
   - AI text optimization and polishing
   - AI error checking and proofreading
   - AI intelligent continuation
   - AI custom editing
   - AI intelligent image search
   - Supports multiple OpenAI-compatible AI models

3. **WordPress Integration Module**
   - Automatic article upload to WordPress
   - Automatic image upload to WordPress media library
   - WordPress REST API authentication support
   - Article categories, tags, and featured image management

4. **Image Management Module**
   - Local image caching mechanism
   - Desktop screenshot function (customizable scaling ratio)
   - Image link local/network mode switching
   - Batch image upload management

5. **User Interface Module**
   - Customizable font size
   - Adjustable zoom ratio
   - Right-click menu shortcuts
   - File drag-and-drop support

### System Architecture Diagrams

#### 1. Overall Architecture
![](images/20260115142542.png)

**Layer Description:**

- **Layer 1 - WinForm Main Interface Layer (Blue)**
  - MainForm provides user interaction interface
  - Contains menu bar, toolbar, status bar, and other UI elements

- **Layer 2 - WebBrowser Control Layer (Yellow)**
  - WebBrowser control hosts IE kernel browser
  - Embeds EditorMD Web framework
  - CodeMirror provides editing functionality, Marked.js provides preview rendering

- **Layer 3 - Adapter and Service Layer (Green)**
  - **JS-C# Bridge**: Implements bidirectional communication between JavaScript and C#
  - **Markdown Adapter**: Handles file, image, and text operations
  - **AI Assistant Module**: Text optimization, error proofreading, intelligent continuation, AI image search
  - **WordPress Module**: Automatic article and image upload
  - **Tool Services**: Screenshot, configuration management, file management

- **Layer 4 - External Resource Layer (Orange)**
  - AI API services (ChatGLM, DeepSeek, OpenAI, etc.)
  - WordPress server
  - Local file system

#### 2. Core Functional Module Components

![](images/20260115144431.png)

#### 3. AI Service Module Detailed Architecture

![](images/20260115144506.png)

#### 4. WordPress Publishing Process Architecture

![](images/20260115144631.png)

#### 5. File and Image Processing Flow

![](images/20260115144702.png)

#### 6. Keyboard Shortcut Mapping

![](images/20260115144720.png)

**Architecture Notes:**

- **Diagram 1 - Overall Layered Architecture**: Shows 5-layer architecture with clear top-down data flow
- **Diagram 2 - Core Functional Modules**: Shows composition of 4 major functional modules (Editing/AI/WordPress/Tools)
- **Diagram 3 - AI Service Module**: Shows complete AI function call chain from user operation to API call
- **Diagram 4 - WordPress Publishing**: Shows complete process from preparation to publication
- **Diagram 5 - File Image Processing**: Shows processing flow for various image input sources and output forms
- **Diagram 6 - Shortcut Key Mapping**: Shows one-to-one correspondence between all shortcuts and functions

### Main Operation Flow Sequence Diagrams

#### 1. AI Text Optimization Flow

![](images/20260115144820.png)

#### 2. WordPress Article Publishing Flow

![](images/20260115144852.png)

#### 3. Image Paste and Insert Flow

![](images/20260115144937.png)

### Source Code Directory Structure:
* MDLoader
	* MDLoader.csproj - C# project file
	* MainForm/
		* Form1.cs - Main form
	* Markdown/
		* Adapter.cs - EditorMD framework adapter
	* Tool/
		* MFiles.cs - Local directory file operations
		* setup.cs - Configuration manager
		* BaiduBrowser.cs - Image search browser
	* WordPress/
		* WordPressAPI.cs - WordPress REST API wrapper
		* WordPressForm.cs - WordPress upload interface
		* WpAdapter.cs - WordPress adapter
		* WordPressHelper.cs - WordPress helper tools
	* AIForms/
		* agent.cs - AI Agent core client
		* AIOnEditorOperation.cs - AI editor operations
		* AICheckListForm.cs - AI proofreading list interface
		* AIReplaceListForm.cs - AI replacement list interface
		* ItemCheckPanel.cs - Proofreading item panel
		* ItemReplacePanel.cs - Replacement item panel
	* version.txt - Version update log
	* bin/ - Release version

### Program Runtime Functional Module Introduction

1. **Prepare Program Runtime Environment (Program.cs)**
	- Parse incoming parameters
	- Adjust default IE runtime environment by editing registry
	- Define cache directory name by timestamp
	- Centralized error message handling

2. **Command Interaction with User (Form1.cs)**
	- Establish WebBrowser runtime environment and perform necessary initialization
	- Load Editor.md runtime framework
	- Load user Markdown file after framework loading succeeds
		- Cache image files locally
		- Load Markdown file to Adapter
		- Refresh WebBrowser to display the file
	- Call methods in Adapter object to respond to user events
		- Open file
			- LoadMDFile method
			- CacheMDPictures method
			- SetUserSideMD method
		- New file
			- Clear method
		- Save file
			- SaveFile method
		- Save as
			- Same as above
		- Sync Markdown workspace size
			- SetClientSize JS method
		- Sync user input and refresh image cache
			- GetUserSideMD method
			- CacheMDPictures method
		- Respond to user Ctrl+V operation
			- Save clipboard to file in image directory, PNG format
			- Insert new image Markdown tag into user edit area
		- Handle user dragging files to application and hyperlink clicks
			- Read dragged file path or clicked hyperlink path through Navigating function
			- Execute operations based on file type
				- First time opening MD file, open in current window
				- Second+ time opening MD file, restart multiple copies of this program to open
				- If it's another file type, call OS Shell program to execute
		- Upload article to WordPress server
			- WordPressForm object call
			- Automatically upload article content and images
		- Switch between local and server image paths
			- SwitchPicture method
		- AI-assisted writing features (V0.9 New)
			- AI text optimization - Polish and optimize selected text
			- AI error checking - Intelligently check text errors and provide suggestions
			- AI continuation - Intelligently continue content based on context
			- AI custom editing - Edit text based on user instructions
			- AI image search - Intelligently search for relevant images
		- Desktop screenshot function (V0.9 New)
			- Support custom scaling ratio
			- Auto-save to image directory
			- Auto-insert Markdown image tag
		- Save user configuration
			- SetupForm object call

3. **Editor.md Adapter (Adapter.cs)**
	- `public List<string> CacheMDPictures(string fileName)`
		- Load images described in Markdown file locally
		- Compare with previous cached directory before each load, only reload image cache when cached images change
	- `LoadMDFile(string fileName, WebBrowser browser)`
		- Load MD file to Adapter
	- `public bool SaveFile(string file)`
		- Save file to specified directory
	- `public void Clear(WebBrowser browser)`
		- Clear Markdown text in Adapter and refresh to browser
	- `public void GetUserSideMD(WebBrowser browser)`
		- Reload MD text from Editor.md to Adapter to prevent user edits from not being updated in time
	- `public void SetUserSideMD(WebBrowser browser)`
		- Load updated MD content to user editor
	- `public void SwitchPicture(WebBrowser browser, Picture_mode type)`
		- Switch image addresses in MD text between local images and network images
	- `public bool GetIfModified()`
		- Check if Markdown text content has been modified since last save
	- `public string GetSelectedText(WebBrowser browser)` (V0.9 New)
		- Get selected text in editor
	- `public string GetTextBeforeSelection(WebBrowser browser)` (V0.9 New)
		- Get content before selected text
	- `public void InsertText(WebBrowser browser, string text)` (V0.9 New)
		- Insert text at current cursor position

4. **WordPress Integration Module (WordPress/)** (V0.8 New)
	- `WordPressAPI.cs` - WordPress REST API core functionality
		- `UploadImageFile()` - Upload image to WordPress media library
		- `CreatePost()` - Create new post
		- `UpdatePost()` - Update post
		- `GetCategories()` - Get category list
		- `GetTags()` - Get tag list
	- `WordPressForm.cs` - WordPress upload interface
		- Post title, category, tag settings
		- Featured image upload
		- Publication status management
	- `WpAdapter.cs` - WordPress and Markdown conversion adapter
		- Markdown to HTML
		- Image link replacement
		- Metadata processing

5. **AI-Assisted Writing Module (AIForms/)** (V0.9 New)
	- `agent.cs` - ChatGLMClient AI client core
		- Support OpenAI-compatible interface
		- Streaming response processing
		- Context management
		- Support custom AI models and API addresses
	- `AIOnEditorOperation.cs` - AI editor operation implementation
		- `AIContinue()` - AI continuation function
		- `AICustomizedModify()` - AI custom editing
		- `AIOptimize()` - AI text optimization
		- `AICheck()` - AI proofreading check
	- `AICheckListForm.cs` - AI proofreading result list interface
		- Display all proofreading suggestions
		- Support viewing and applying modifications item by item
	- `AIReplaceListForm.cs` - AI replacement suggestion list interface
		- Display optimized text comparison
		- Support selective application of modifications
	- `ItemCheckPanel.cs` / `ItemReplacePanel.cs` - Check/Replace item UI panels
		- Single proofreading/replacement content display
		- Interactive operation support

6. **Browser Environment Simulator Configuration (SetWebbrowser.cs)**
	- `ChangeWebbrowserMode(int ieMode)`
		- Modify IE version simulated by WebBrowser control

7. **Configuration Manager (setup.cs)** (Continuous Updates)
	- WordPress configuration
		- Server address, username, application password
	- AI configuration (V0.9 New)
		- AI API Key
		- AI API address (supports OpenAI-compatible interface)
		- AI model selection
	- Interface configuration
		- Font size
		- Zoom ratio
		- Screenshot zoom ratio (V0.9 New)
	- Editor configuration
		- Default save path
		- Image cache path

8. **Image Search Browser (BaiduBrowser.cs)** (V0.9 New)
	- Integrate online image search
	- Image preview and download
	- Auto-insert into Markdown document

## Technical Features

1. **Cross-Platform Compatibility**
   - Supports Windows 7/10/11
   - Auto-adapts to different DPI scaling ratios
   - Compatibility registry configuration

2. **AI Integration Architecture**
   - Standard OpenAI API compatible interface
   - Supports multiple AI models (ChatGLM, DeepSeek, etc.)
   - Asynchronous streaming response processing
   - Configurable AI parameters

3. **WordPress Integration**
   - Based on REST API v2
   - Secure application password authentication
   - Automatic image upload and link replacement
   - Complete article metadata support

4. **Modular Design**
   - Clear functional module division
   - Loosely coupled architectural design
   - Easy to extend and maintain

## Version Evolution

- V0.9 (Current Version)
  - Added AI-assisted writing features
  - Added AI image search feature
  - Added AI proofreading feature
  - Added desktop screenshot feature
  - Added series of writing-assistance right-click menu features

- V0.8
  - Removed FTP upload function, replaced with WordPress upload
  - Added article upload to WordPress server
  - Added freely adjustable menu bar and edit bar font size
  - Fixed layout disorder issue after text zoom

- V0.6
  - Fixed hyperlink opening issue
  - Optimized solution for intermittent file display failure
  - Supports file drag-and-drop opening
  - Win11 environment testing passed
  - Release version added Loader for online updates

## Future Plans

1. Support more AI models and providers
2. Enhanced WordPress integration (draft sync, offline editing, etc.)
3. Support more image hosting services
4. Add plugin system for third-party extensions
5. Cloud synchronization functionality
6. Multi-language internationalization support

## Development Notes

This project is open source under the MIT license. Contributions and suggestions are welcome.

---

For more detailed information, please read the comments in the source code directly. If you have questions while reading the code, feel free to contact me. QQ: 64034373. Please note "MDFileLoader" in friend requests. Due to limited capabilities, bugs are inevitable. Bug reports are welcome, and contributors with significant contributions will be added to the contributor list.
