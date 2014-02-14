#import "LargeCrate.h"



static NSString * ImgCrate = @"CrateWithLabel.png";
static Player * _player;
static Liselot * _liselot;



@implementation LargeCrate

@synthesize isAboutToExplode;

+ (id) largeCrateWithOrigin:(CGPoint)Origin  withPlayer:(Player *)player  withLiselot:(Liselot *)liselot
{
	return [[[self alloc] initWithOrigin:Origin  withPlayer:(Player *)player  withLiselot:(Liselot *)liselot] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin  withPlayer:(Player *)player  withLiselot:(Liselot *)liselot
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
		
		[self loadGraphicWithParam1:ImgCrate param2:YES param3:NO param4:80 param5:60];
        //self.acceleration = CGPointMake(0, 800);
        self.width = 80;
        self.height = 60;
        //self.drag = CGPointMake(500, 500);
        self.fixed=YES;
        _player = player;
        _liselot=liselot;
        self.originalXPos=Origin.x;
        self.originalYPos=Origin.y;

        
	}
	
	return self;	
}


- (void) dealloc
{
    
	[super dealloc];
}

- (void) hitLeftWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    
    //[super hitLeftWithParam1:Contact param2:Velocity];
    if (_player.isAirDashing || _liselot.isAirDashing ) {
        //self.x = -1000;
        //self.y = -1000;
        isAboutToExplode=YES;

    }
    
    
}



- (void) hitRightWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    //[super hitLeftWithParam1:Contact param2:Velocity];
    if (_player.isAirDashing || _liselot.isAirDashing ) {
        //self.x = -1000;
        //self.y = -1000;
        
        isAboutToExplode=YES;

    }
}
- (void) moveOffScreen
{
    self.x=-1000;
    self.y=-1000;
    
    [FlxG playWithParam1:@"grenadeExplosion.caf" param2:0.35 param3:NO];

    [FlxG vibrate];

}

- (void) update
{   

	[super update];
    
    isAboutToExplode=NO;

	
}


@end
