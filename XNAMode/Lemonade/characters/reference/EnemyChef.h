//
//  Enemy.h
//  FlxShmup
//
//  Created by Shane Brouwer on 7/04/11.
//  Copyright 2011 Initials. All rights reserved.
//
#import "Enemy.h"

@interface EnemyChef : Enemy
{
    //NSArray * animationList;
    
}

+ (id) enemyChefWithOrigin:(CGPoint)Origin index:(int)Index;
- (id) initWithOrigin:(CGPoint)Origin index:(int)Index;
- (void) killAddStat;


@end
