# vdjcopy

A simple application that copies songs from a VirtualDJ-generated .m3u file to a target folder.

Usage: 
`vdjcopy [m3u_path] [destination_path]`

example:
`vdjcopy C:\Users\John\Documents\VirtualDJ\Playlists\yearmix.m3u C:\__YEARMIX2023`

This will copy all the songs in the playlist and rename them using the following template
{no.} {Artist}-{Title} ({Remix})

example:
`008. BetweenUs-Sonnar (Extended Mix).mp3`

## Motivation

Every year I do the yearmix, I have to constantly "right-click, open folder" each time i want to drag my song into my DAW (Studio One) and that's just BORING. I prepare my yearmix setlist in Virtual DJ and then copy all the files into a folder that I can pin in Studio One, no need to even open Explorer.
