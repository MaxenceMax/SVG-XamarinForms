# SVG-XamarinForms

# Usage
Instead of using an Image Control simply use SvgImage!

### You MUST :

- Set Height or Width
- Set Build Action to Embedded Resource for your svg files 
- Source to {Project}.{Folder}.{FileName}.{Extension}
- Assembly to the assembly containing the file
 - Example : If you have Sample.svg also add Sample.xaml under the same folder of the svg.(No need to specify anything else as long as the names match)	


Xaml Usage : 

add the using :
```
    xmlns:svg="clr-namespace:SVG.Xamarin.Forms;assembly=SVG.Xamarin.Forms"
```

```
  <svg:SvgIcon 
      HeightRequest="150"
      Assembly="{Binding Assembly}"
      Source="{Project}.{Folder}.{FileName}.{Extension}"/>
```

In the example my SVG file is located under the Images folder in the PluginSampleApp project. The SvgAssembly is a reference to the assembly containing the svg/xaml file.