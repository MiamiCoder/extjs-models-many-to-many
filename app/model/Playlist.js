Ext.define('App.model.Playlist', {
    extend: 'Ext.data.Model',
    idProperty:'PlaylistId',
    fields: [
        { name: 'PlaylistId', type: 'int' },
        { name: 'Name', type: 'string' }
    ],
    proxy: {        // Note that proxy is defined in the Model, not the Store
        type: 'rest',
        url: '../../../api/PlaylistsAssociationsManyToManyExample',
        reader: {
            type: 'json',
            rootProperty: 'playlists',
            totalProperty: 'count'
        }
    },
    manyToMany: {
        TrackPlaylists: {
            type: 'App.model.Track',    // The type of the model related to this model.
            role: 'tracks',             // Creates a "tracks()" function that returns the Tracks Store.
            field: 'TrackId',           // The primary key of the related model.
            right: {                    // The model related to the Track model.  (Now we are defining the relationship in the other direction.)
                field: 'PlaylistId',    // The primary key of the related model.
                role: 'playlists'       // Creates a "playlists()" function in the Track model that returns the Palylists Store.
            }
        }
    }
});