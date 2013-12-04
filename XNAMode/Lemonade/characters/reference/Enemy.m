#import "Enemy.h"

#import "Andre.h"
#import "Liselot.h"
#import "EnemyChef.h"

#define POINTS_FOR_HURT 0
#define POINTS_FOR_KILL 0
#define ORIGINAL_HEALTH 0

#define GRAVITY 1800
#define DRAG 1900
#define GRAVITYREMOVEMAXTIME 0.25


@implementation Enemy



@synthesize timer;
@synthesize originalHealth;
@synthesize index;
@synthesize limitX;
@synthesize limitY;
@synthesize isTalking;
@synthesize originalVelocity;

@synthesize canRun;

@synthesize lastVelocity;
@synthesize lastPosition;
@synthesize lastX;


+ (id) enemyWithOrigin:(CGPoint)Origin index:(int)Index
{
	return [[[self alloc] initWithOrigin:Origin index:Index] autorelease];
}

- (id) initWithOrigin:(CGPoint)Origin index:(int)Index
{
	if ((self = [super initWithX:Origin.x y:Origin.y graphic:nil])) {
		
        //speechBubble = [[FlxSprite spriteWithX:0 y:0 graphic:@"speechBubbleTiles.png"] retain];
        self.gravityRemovalTimer = 0.0;
        self.gravityRemovalMaxTimeAllowed = 2.0;
        
        
		//gravity
		self.acceleration = CGPointMake(0, 1800);
		self.velocity = CGPointMake(20, 0);
		//self.drag = CGPointMake(10, 10);
        
        self.originalXPos = Origin.x;
        self.originalYPos = Origin.y;
        isTalking=NO;
        canRun = YES;

        maxVelocity.x = 300;
        maxVelocity.y = 430;
        
	}
	
	return self;	
}

- (void) hitLeftWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    //NSLog(@"hit left");
    //velocity.x = -velocity.x;
    
    //determine direction
    
    
    
    if ([Contact isKindOfClass:[Andre class]] ||[Contact isKindOfClass:[Liselot class]] ) {
        FlxSprite * cont = (FlxSprite *)Contact;

        if (cont.scale.x==1 && self.scale.x ==1 )
        {
            return;
        }
        else if (cont.scale.x==-1 && self.scale.x ==-1)
        {
            return;
        }
    }
    
    
    CGFloat i = self.velocity.x * -1;
    self.velocity = CGPointMake(i, 0);
    
    //self.x = self.x + 10;
    
    //[super hitLeftWithParam1:Contact param2:Velocity];
}

- (void) hitRightWithParam1:(FlxObject *)Contact param2:(float)Velocity
{
    //NSLog(@"hit Right vel %f", self.velocity.x);
    
    if ([Contact isKindOfClass:[Andre class]] ||[Contact isKindOfClass:[Liselot class]] ) {
        FlxSprite * cont = (FlxSprite *)Contact;
        
        if (cont.scale.x==1 && self.scale.x ==1 )
        {
            return;
        }
        else if (cont.scale.x==-1 && self.scale.x ==-1)
        {
            return;
        }
    }
    
    
    
    
    CGFloat i = self.velocity.x * -1;
    self.velocity = CGPointMake(i, 0);
    //self.x = self.x -= 20;
    
    

    //self.x = self.x - 10;
    
    //[super hitRightWithParam1:Contact param2:Velocity];
}

- (void) hitBottomWithParam1:(FlxObject *)Contact param2:(float)Velocity
{

    
    [super hitBottomWithParam1:Contact param2:Velocity];
    
    if (self.velocity.y<1) {
        self.y = roundf(self.y);
    }
}




- (void) dealloc
{
	//[speechBubble release];
	[super dealloc];
}

- (void) hurt:(float)Damage
{
    [self flicker:0.25];
    self.health -= Damage;
    if (self.health <= 0 && self.dead == NO) {
        dead = YES;
        //visible = NO;
        //self.x = 1000;
        //self.y = 1000;
        self.velocity = CGPointMake(self.velocity.x,-250);
        self.angularVelocity = self.velocity.x*2;
        [self flicker:0.75];
        //self.acceleration = CGPointMake(0,0);

    }
    else {
        [FlxG play:@"deathSFX.caf"];

    }
}

- (NSString *) getCurAnim {
    return _curAnim.name;
}

- (void) run {
    if (canRun && self.onFloor) {
        self.velocity = CGPointMake(self.velocity.x*2.45, self.velocity.y);
        canRun=NO;
    }
}

- (void) runAndJump 
{
    [self runAndJump:400];
    
}

- (void) runAndJump:(float)jumpHeight
{
    if (canRun && self.onFloor) {
        self.velocity = CGPointMake(self.velocity.x*2.45, jumpHeight * -1);
        canRun=NO;
    }
}

- (void) runJumpAndTurn:(float)jumpHeight;
{
    if (canRun && self.onFloor) {
        self.velocity = CGPointMake(self.velocity.x*-2.45, jumpHeight * -1);
        self.scale=CGPointMake(self.scale.x*-1, self.scale.y);
        canRun=NO;
    }
}


- (void) render
{
    //[speechBubble render];
    [super render];
}

- (void) talk:(BOOL)startTalking
{
    if (startTalking) {
        isTalking=YES;
        self.velocity=CGPointMake(0, 0);
        [self play:@"talk"];
    }
    else if (!startTalking) {
        [self play:@"walk"];
        isTalking=NO;
        if (self.scale.x==1) {
            self.velocity=CGPointMake(originalVelocity,0 );
        } else if (self.scale.x==-1) {
            self.velocity=CGPointMake(originalVelocity*-1,0 );
        }
    }
}


- (void) update
{   

//    self.y = roundf(self.y);
    
    
    if (self.dead) {
        self.velocity = CGPointMake(0, self.velocity.y);
        self.gravityRemovalTimer += 100000;
        
        
        [self play:@"death"];
    }
    
    //    if (self.x >= limitX ) {
//        self.x = limitX-1;
//        CGFloat i = self.velocity.x * -1;
//        //self.velocity = CGPointMake(i, 0);
//        
//        self.velocity = CGPointMake(-self.originalVelocity, self.velocity.y);
//        [self play:@"walk"];
//        
//        //NSLog(@">=");
//        //NSLog(@"x==%f, oX==%f", self.x, limitX);
//
//        
//        canRun=YES;
//    } else if (self.x <= originalXPos) {
//        self.x = originalXPos+1;
//        CGFloat i = self.velocity.x * -1;
//        //self.velocity = CGPointMake(i, 0);  
//        
//        self.velocity = CGPointMake(self.originalVelocity, self.velocity.y);
//        [self play:@"walk"];
//        
//        //NSLog(@"<=");
//        //NSLog(@"x==%f, oX==%f", self.x, originalXPos);
//
//        
//        canRun=YES;
//    }
    
    if (!isTalking) {
        if (self.velocity.x >= 0) {
            self.scale = CGPointMake(1, 1);
        }
        else {
            self.scale = CGPointMake(-1, 1);
        }
    }
    
    // Wrap around levels.
    
    if (self.y > FlxG.levelHeight) {
        self.y = 0;
    }
    if (self.y < 0) {
        self.y = FlxG.levelHeight;
    }
    if (self.x > FlxG.levelWidth) {
        self.x = 0;
    }
    if (self.x < 0) {
        self.x = FlxG.levelWidth;
    }
    

    if (self.velocity.y > 0 && lastVelocity == 0.0)
    {
        //NSLog(@"About To fall");
        
        self.velocity = CGPointMake(self.velocity.x*-1, self.velocity.y);
        self.scale=CGPointMake(self.scale.x*-1, self.scale.y);
        self.x = lastPosition.x;
        self.y = lastPosition.y - 3;
        
        
        
    }
    
    if (self.x == lastX)
    {
        self.velocity = CGPointMake(self.velocity.x*-1, self.velocity.y);
        self.scale=CGPointMake(self.scale.x*-1, self.scale.y);
    
    }
    
    lastVelocity = self.velocity.y;
    lastX = self.x;
    
    
	[super update];
    //NSLog(@"bottom update vel %f", self.velocity.x);
    
    
    
    lastPosition = CGPointMake(self.x, self.y);
    
    if ([_curAnim.name isEqualToString:@"death"] && _curFrame==1) {
        [self killAddStat];
    }

}
- (void) killAddStat
{
    
    
}

@end
