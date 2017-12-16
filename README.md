# WinFilters
A Windows Winforms program to visualize various combinations of electrical/acoustic filters, lowpass, highpass and summed response in graphical form. It's primarily for acoustic filters as might be used in 2-way loudspeaker systems. Written in C#, it makes use of Zedgraph, the open source graphing package.

![Bessel Flattest Sum](https://user-images.githubusercontent.com/1345527/33976297-f1f4a8e0-e061-11e7-8a2d-bbf5fd7685e5.gif)

I've been changing solutions from VS2015 to VS2017. You may need 2017 to build this.

## Filters Included

- Linkwitz-Riley
- Butterworth
- Bessel

## Electrical Filter Considerations

As noted, WinFilters visualizes electrical and acoustical filters. However, there is no use for anything other than Bessel and simple Butterworth filters in the electrical domain to my knowledge. I'm also not aware of any case in which electrical filters would be used in a summed situation as is almost ubiquitous in loudspeaker acoustics. That said, when the offset is zero and/or the Filter To Offset is set to "None", this will be both the electrical and equidistance acoustic response.

## Basic Notes on Loudspeaker Design

A number of versions of Bessel filters are included, the one described as Flattest is shown above. In reality, Bessel filters would not be used for acoustic filters, but I included them because the spreadsheet that this program originally emulated included a version. I added other versions on something of a whim since I was already working on one. What is interesting about them is that in all multi-way loudspeakers there will always be excess-delay differences between time-of-arrival from the various drivers at any microphone/listening position. The intended listening position is primary for design purposes. 

Most loudspeaker systems have unequal delays to the design point and filters are adjusted to some degree to optimize to the listening position.  Linkwitz-Riley and Butterworth odd-order filters will sum flat on-axis only if all driver delays are equal. Any difference in delay can be partially compensated by filter changes, a common practice in loudspeaker design. It's interesting to see that at times the optimal summed response will require one filter, say the lowpass in a Linkwitz-Riley target design for example, that is actually closer to a Bessel filter. I added all of the Bessel filters to make it easy to mix and match filters and delays to see the result and to confirm that I had implemented them correctly before putting the code into my loudspeaker design program, link below.

[WinPCD - Loudspeaker Passive Crossover Design](http://www.speakerdesign.net/WinPCD/index.htm)
