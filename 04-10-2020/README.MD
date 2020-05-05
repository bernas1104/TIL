# Today I Learned

We're on the third day of the GoStack Bootcamp today, and we're taking a look at
React Native. It uses the same ideas of ReactJS - like states and immutability -,
but has some differences.

Differences like:

- While ReactJS uses HTML tags that contain semantic value, React Native doesn't
use HTML tags. It uses its own types of tags like: View, Text, and so on. It's up
to the developer to style theses tags and define their purpose;
- All the React Native components have the "display: flex" built in to them;
- There is no .css (Cascade Style Sheet) files in React Native. Instead, developers
import the StyleSheet from the React Native lib, and build a type of javascript
object that contains the same properties of CSS.
    - Except that they don't use -, so when you use something like 'font-family',
you have to remove the '-' and use camel case, like so: fontFamily.

Developers need to pay attention to what type of setup they are using. If you are
using a emulator or physical device, there's a chance that the address needed to
access your local API will change according to the setup you got.