Ext.application({
    name: 'App',
    models: ['Track', 'Playlist'],
    stores: ['Tracks', 'Playlists'],
    launch: function () {

        var playlistsStore = Ext.getStore('Playlists');
        playlistsStore.load(function (records, operation, success) {

            var playlist = records[0],
                tracks = playlist.tracks(),
                track;

            tracks.load(function (records, operation, success) {
                console.log('Tracks for Playlist: ' + playlist.get('Name') + '\n-------------------------------------------------------');
                for (i = 0, len = records.length; i < len; i++) {
                    track = records[i];
                    console.log('Track Name: ' + track.get('Name'));
                    console.log('-------------------------------');
                }
            });
        });


        var tracksStore = Ext.getStore('Tracks');
        tracksStore.load(function (records, operation, success) {

            var track = records[0],
                playlists = track.playlists(),
                playlist;

            playlists.load(function (records, operation, success) {
                console.log('Playlists for Track: ' + track.get('Name') + '\n-------------------------------------------------------');
                for (i = 0, len = records.length; i < len; i++) {
                    playlist = records[i];
                    console.log('Playlist Name: ' + playlist.get('Name'));
                    console.log('-------------------------------');
                }
            });
        });
    }
});